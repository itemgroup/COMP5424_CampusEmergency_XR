"""Rebuild the project's original SVG/PNG labels, simple props and soft WAV cues.

No third-party artwork, icons, models or recorded sound is used by this script.
Pillow is required for PNG output. Coordinates in OBJ files use metres, Y up.
"""
from pathlib import Path
import math
import struct
import wave
import json
import hashlib
from PIL import Image, ImageDraw, ImageFont

ROOT = Path(__file__).resolve().parents[1]
BASE = ROOT / 'Assets' / 'Module5'
SIGNS = BASE / 'Original' / 'Signage'
MODELS = BASE / 'Original' / 'Models'
AUDIO = BASE / 'Original' / 'Audio'
PREVIEW = ROOT / 'Preview'
for folder in (SIGNS, MODELS, AUDIO, PREVIEW):
    folder.mkdir(parents=True, exist_ok=True)

FONT = Path('/System/Library/Fonts/Supplemental/Arial Bold.ttf')
if not FONT.exists():
    FONT = Path('/usr/share/fonts/truetype/dejavu/DejaVuSans-Bold.ttf')
W, H = 1200, 500

labels = [
    ('exit_left', 'EXIT', '', 'left', '#16644C'),
    ('exit_right', 'EXIT', '', 'right', '#16644C'),
    ('stairs_left', 'STAIRS', '', 'left', '#16644C'),
    ('stairs_right', 'STAIRS', '', 'right', '#16644C'),
    ('level_4', 'LEVEL 4', '', None, '#1E4155'),
    ('level_1', 'LEVEL 1', '', None, '#1E4155'),
    ('training_endpoint', 'TRAINING ENDPOINT', 'LEVEL 1', None, '#16644C'),
    ('elevator_unavailable', 'ELEVATOR UNAVAILABLE', 'IN THIS EXERCISE', None, '#5A4527'),
]

def fit_font(text, width, maximum):
    for size in range(maximum, 30, -2):
        font = ImageFont.truetype(str(FONT), size)
        if font.getbbox(text)[2] <= width:
            return font
    return ImageFont.truetype(str(FONT), 32)

def arrow_points(direction):
    points = [(70,250),(205,110),(205,195),(345,195),(345,305),(205,305),(205,390)]
    return points if direction == 'left' else [(W-x,y) for x,y in points]

for name, title, subtitle, direction, bg in labels:
    im = Image.new('RGB', (W,H), bg)
    draw = ImageDraw.Draw(im)
    draw.rounded_rectangle((18,18,W-18,H-18), radius=18, outline='white', width=8)
    if direction:
        draw.polygon(arrow_points(direction), fill='white')
        x = 770 if direction == 'left' else 430
        text_width = 670
    else:
        x, text_width = W//2, W-110
    font = fit_font(title, text_width, 142 if not subtitle else 92)
    draw.text((x,230 if subtitle else 250), title, font=font, fill='white', anchor='mm')
    if subtitle:
        draw.text((W//2,345), subtitle, font=fit_font(subtitle,W-120,60), fill='white', anchor='mm')
    im.save(SIGNS / f'{name}.png')
    points = ' '.join(f'{px},{py}' for px,py in arrow_points(direction)) if direction else ''
    svg = f'''<svg xmlns="http://www.w3.org/2000/svg" width="1200" height="500" viewBox="0 0 1200 500">
<rect width="1200" height="500" fill="{bg}"/><rect x="18" y="18" width="1164" height="464" rx="18" fill="none" stroke="white" stroke-width="8"/>
{f'<polygon points="{points}" fill="white"/>' if direction else ''}
<text x="{x}" y="{230 if subtitle else 250}" dominant-baseline="central" text-anchor="middle" font-family="Arial, sans-serif" font-weight="bold" font-size="{font.size}" fill="white">{title}</text>
{f'<text x="600" y="345" dominant-baseline="central" text-anchor="middle" font-family="Arial, sans-serif" font-weight="bold" font-size="60" fill="white">{subtitle}</text>' if subtitle else ''}
</svg>'''
    (SIGNS / f'{name}.svg').write_text(svg, encoding='utf-8')

sheet = Image.new('RGB',(1240,1280),'#e9ecef')
sd = ImageDraw.Draw(sheet)
for index,(name,*_) in enumerate(labels):
    thumb = Image.open(SIGNS / f'{name}.png').resize((580,242))
    x,y = 30+(index%2)*620,25+(index//2)*310
    sheet.paste(thumb,(x,y))
    sd.text((x,y+250),name+'.png',fill='#18313e',font=ImageFont.truetype(str(FONT),24))
sheet.save(PREVIEW / 'signage_contact_sheet.png')

MATERIALS = {'Door':(.43,.48,.53),'Frame':(.20,.24,.28),'Wall':(.79,.81,.81),'SignBlank':(1,1,1)}
mtl = '\n'.join(f'newmtl {name}\nKa 0.1 0.1 0.1\nKd {r} {g} {b}\nKs 0 0 0\nd 1\n' for name,(r,g,b) in MATERIALS.items())
(MODELS / 'training_original.mtl').write_text(mtl,encoding='utf-8')

def model(name, boxes):
    lines = ['# Original procedural prop, metres, Y up', 'mtllib training_original.mtl',f'o {name}']
    vertex = 1
    triangles = 0
    for part,material,center,size in boxes:
        x,y,z=center; sx,sy,sz=[v/2 for v in size]
        faces = [
            [(x-sx,y-sy,z+sz),(x+sx,y-sy,z+sz),(x+sx,y+sy,z+sz),(x-sx,y+sy,z+sz)],
            [(x+sx,y-sy,z-sz),(x-sx,y-sy,z-sz),(x-sx,y+sy,z-sz),(x+sx,y+sy,z-sz)],
            [(x+sx,y-sy,z+sz),(x+sx,y-sy,z-sz),(x+sx,y+sy,z-sz),(x+sx,y+sy,z+sz)],
            [(x-sx,y-sy,z-sz),(x-sx,y-sy,z+sz),(x-sx,y+sy,z+sz),(x-sx,y+sy,z-sz)],
            [(x-sx,y+sy,z+sz),(x+sx,y+sy,z+sz),(x+sx,y+sy,z-sz),(x-sx,y+sy,z-sz)],
            [(x-sx,y-sy,z-sz),(x+sx,y-sy,z-sz),(x+sx,y-sy,z+sz),(x-sx,y-sy,z+sz)],
        ]
        lines += [f'g {part}',f'usemtl {material}']
        for face in faces:
            for point in face:
                lines.append('v '+' '.join(f'{v:.6f}' for v in point))
            lines += [f'f {vertex} {vertex+1} {vertex+2}',f'f {vertex} {vertex+2} {vertex+3}']
            vertex += 4; triangles += 2
    (MODELS / f'{name}.obj').write_text('\n'.join(lines)+'\n',encoding='utf-8')
    return {'file':f'{name}.obj','vertex_count':vertex-1,'triangle_count':triangles}

specs = [model('TrainingElevatorFacade',[
    ('leftDoor','Door',(-.405,1.05,0),(.8,2.1,.04)),
    ('rightDoor','Door',(.405,1.05,0),(.8,2.1,.04)),
    ('leftFrame','Frame',(-.88,1.14,0),(.12,2.28,.1)),
    ('rightFrame','Frame',(.88,1.14,0),(.12,2.28,.1)),
    ('topFrame','Frame',(0,2.22,0),(1.88,.12,.1)),
    ('callPanel','Frame',(1.06,1.1,.02),(.15,.28,.055)),
])]
# A single front-facing textured quad. Import it as a static visual, not a collider.
(MODELS / 'TrainingSignPlate.obj').write_text('''# Original front-facing quad, 1.2 m x 0.5 m, normal +Z
mtllib training_original.mtl
o TrainingSignPlate
v -0.6 -0.25 0
v 0.6 -0.25 0
v 0.6 0.25 0
v -0.6 0.25 0
vt 0 0
vt 1 0
vt 1 1
vt 0 1
vn 0 0 1
usemtl SignBlank
f 1/1/1 2/2/1 3/3/1
f 1/1/1 3/3/1 4/4/1
''',encoding='utf-8')
specs.append({'file':'TrainingSignPlate.obj','vertex_count':4,'triangle_count':2})

def write_tone(name, duration, notes, gain):
    rate=44100
    samples=[]
    for index in range(round(duration*rate)):
        t=index/rate; value=0
        for start,length,frequency in notes:
            local=t-start
            if 0<=local<length:
                envelope=min(1,local/.035,(length-local)/.06)
                value += gain*envelope*math.sin(2*math.pi*frequency*local)
        samples.append(max(-1,min(1,value)))
    with wave.open(str(AUDIO / f'{name}.wav'),'wb') as out:
        out.setnchannels(1);out.setsampwidth(2);out.setframerate(rate)
        out.writeframes(b''.join(struct.pack('<h',round(value*32767)) for value in samples))
    peak=max(abs(value) for value in samples)
    rms=math.sqrt(sum(value*value for value in samples)/len(samples))
    return {'file':name+'.wav','seconds':duration,'sample_rate':rate,'channels':1,'bits':16,'peak_dbfs':round(20*math.log10(peak),2),'rms_dbfs':round(20*math.log10(rms),2)}

sounds = [
    write_tone('training_alert_soft',1.7,[(.10,.32,440),(.65,.32,440),(1.20,.32,440)],.12),
    write_tone('ui_confirm_soft',.22,[(.02,.13,660)],.09),
    write_tone('hint_soft',.48,[(.02,.28,523.25)],.09),
    write_tone('completion_soft',.75,[(.02,.20,523.25),(.26,.20,659.25),(.50,.20,783.99)],.09),
]

report = {'created':'2026-10-01','creator':'Codex at the user\'s direction','source_type':'Original procedural content, no sampled audio or copied artwork','models':specs,'audio':sounds,'signs':[entry[0]+'.png' for entry in labels]}
(ROOT / 'Research' / 'original_assets_specs.json').parent.mkdir(parents=True,exist_ok=True)
(ROOT / 'Research' / 'original_assets_specs.json').write_text(json.dumps(report,indent=2),encoding='utf-8')
print(json.dumps(report,indent=2))
