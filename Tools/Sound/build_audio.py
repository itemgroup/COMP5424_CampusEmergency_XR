"""Deterministic, locally generated sound assets. Python 3 + numpy; no network.
Run Build-Voices.ps1 first (raw TTS), then this script ONCE to process those WAVs.
"""
import json, math, pathlib, wave, hashlib
import numpy as np
ROOT = pathlib.Path(__file__).resolve().parents[2]
OUT = ROOT / 'Assets/Resources/CampusSound'
OUT.mkdir(parents=True, exist_ok=True)
SR = 22050
rng = np.random.default_rng(5424)
def save(name, samples, peak=.65):
    x=np.asarray(samples,dtype=float)
    x=x/max(float(np.max(np.abs(x))),1e-8)*peak
    with wave.open(str(OUT/(name+'.wav')),'wb') as w:
        w.setnchannels(1); w.setsampwidth(2); w.setframerate(SR)
        w.writeframes(np.round(np.clip(x,-1,1)*32767).astype('<i2').tobytes())
def bell(f, duration=.45):
    t=np.arange(int(SR*duration))/SR
    env=np.minimum(t/.015,1)*np.exp(-t*7)*np.minimum((duration-t)/.03,1)
    return env*(np.sin(2*np.pi*f*t)+.16*np.sin(2*np.pi*f*2*t)+.035*np.sin(2*np.pi*f*3*t))
def sequence(notes,duration):
    x=np.zeros(int(SR*duration))
    for start,f,length,gain in notes:
        y=bell(f,length)*gain; i=int(start*SR); x[i:i+len(y)]+=y[:len(x)-i]
    return x
# A soft building/metro-style descending two-note pattern, not a sampled MTR recording.
save('alarm_loop',sequence([(0.05,659.25,.65,1),(.82,554.37,.65,.9),(2.0,659.25,.65,1),(2.77,554.37,.65,.9)],4.5),.42)
save('alarm_activation',sequence([(0,440,.35,.7),(.3,659.25,.45,.8)],.9),.5)
save('popup',sequence([(0,740,.25,.7),(.13,880,.22,.45)],.4),.5)
save('correct',sequence([(0,523.25,.35,.7),(.18,659.25,.35,.7),(.36,783.99,.4,.7)],.85),.55)
save('incorrect',sequence([(0,349.23,.3,.8),(.25,293.66,.35,.7)],.7),.5)
save('completion',sequence([(0,523.25,.6,.7),(.2,659.25,.6,.7),(.4,783.99,.65,.7),(.65,1046.5,.7,.4)],1.5),.6)
t=np.arange(int(.55*SR))/SR
env=np.sin(np.pi*t/.55)**2
phase=2*np.pi*(300*t+330*t*t)
save('teleport',env*(.7*np.sin(phase)+.12*rng.normal(0,1,len(t))),.45)
for surface in ('floor','stair'):
    for i in range(1,4):
        duration=.26 if surface=='floor' else .32
        t=np.arange(int(duration*SR))/SR
        noise=rng.normal(0,1,len(t)); smooth=np.convolve(noise,np.ones(14)/14,'same')
        thud=np.sin(2*np.pi*(85+i*7)*t)*np.exp(-t*35)
        scuff=smooth*np.exp(-t*19)
        heel=np.exp(-((t-.075)/.025)**2)*smooth*.5
        x=(thud*.65+scuff*.6+heel)*np.minimum(t/.005,1)*np.minimum((duration-t)/.03,1)
        if surface=='stair': x+=.12*np.sin(2*np.pi*180*t)*np.exp(-t*17)
        save(f'step_{surface}_{i}',x,.55)
# Periodic low-band outdoor wind. FFT filtering makes the loop boundary continuous.
n=SR*12; white=rng.normal(0,1,n)
freq=np.fft.rfftfreq(n,1/SR); spectrum=np.fft.rfft(white)
gain=(1-np.exp(-freq/45))*np.exp(-(freq/650)**2)
wind=np.fft.irfft(spectrum*gain,n=n)
wind*=.7+.3*np.sin(2*np.pi*np.arange(n)/n)
save('outdoor_wind',wind,.22)
voices=json.loads((ROOT/'Tools/Sound/voices.json').read_text(encoding='utf-8'))['voices']
for entry in voices:
    path=OUT/(entry['clip']+'.wav')
    with wave.open(str(path),'rb') as w:
        assert w.getframerate()==SR and w.getsampwidth()==2 and w.getnchannels()==1
        x=np.frombuffer(w.readframes(w.getnframes()),dtype='<i2').astype(float)/32768
    if entry['pa']:
        f=np.fft.rfftfreq(len(x),1/SR)
        band=(1-np.exp(-(f/180)**2))*np.exp(-(f/4200)**4)
        dry=np.fft.irfft(np.fft.rfft(x)*band,n=len(x))
        dry=np.tanh(dry*1.15)/1.15
        x=np.pad(dry,(0,int(.35*SR)))
        for delay,gain in ((.045,.1),(.092,.075),(.16,.05),(.24,.025)):
            offset=int(delay*SR); x[offset:offset+len(dry)]+=dry*gain
    x=np.pad(x,(int(.1*SR),int(.18*SR)))
    save(entry['clip'],x,.72)
(OUT/'voice_catalog.json').write_text(json.dumps({'entries':[{'text':e['text'],'clip':e['clip']} for e in voices]},ensure_ascii=False,indent=2)+'\n',encoding='utf-8')
manifest=[]
for path in sorted(OUT.glob('*.wav')):
    with wave.open(str(path),'rb') as w:
        samples=np.frombuffer(w.readframes(w.getnframes()),dtype='<i2')/32768
        manifest.append({'file':path.name,'seconds':round(w.getnframes()/w.getframerate(),3),'sample_rate':w.getframerate(),'peak_dbfs':round(20*math.log10(max(float(np.max(np.abs(samples))),1e-8)),2),'sha256':hashlib.sha256(path.read_bytes()).hexdigest()})
(ROOT/'Tools/Sound/audio_manifest.json').write_text(json.dumps(manifest,indent=2)+'\n',encoding='utf-8')
print(f'Generated and measured {len(manifest)} mono WAV assets.')
