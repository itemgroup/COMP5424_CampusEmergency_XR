#include "pch-cpp.hpp"





template <typename T1>
struct VirtualActionInvoker1
{
	typedef void (*Action)(void*, T1, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeObject* obj, T1 p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		((Action)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};
template <typename T1>
struct GenericVirtualActionInvoker1
{
	typedef void (*Action)(void*, T1, const RuntimeMethod*);

	static inline void Invoke (const RuntimeMethod* method, RuntimeObject* obj, T1 p1)
	{
		VirtualInvokeData invokeData;
		il2cpp_codegen_get_generic_virtual_invoke_data(method, obj, &invokeData);
		((Action)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};
template <typename T1>
struct InterfaceActionInvoker1
{
	typedef void (*Action)(void*, T1, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj, T1 p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		((Action)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};
template <typename R>
struct InterfaceFuncInvoker0
{
	typedef R (*Func)(void*, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		return ((Func)invokeData.methodPtr)(obj, invokeData.method);
	}
};
template <typename R, typename T1, typename T2, typename T3>
struct InterfaceFuncInvoker3
{
	typedef R (*Func)(void*, T1, T2, T3, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj, T1 p1, T2 p2, T3 p3)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		return ((Func)invokeData.methodPtr)(obj, p1, p2, p3, invokeData.method);
	}
};
template <typename T1>
struct GenericInterfaceActionInvoker1
{
	typedef void (*Action)(void*, T1, const RuntimeMethod*);

	static inline void Invoke (const RuntimeMethod* method, RuntimeObject* obj, T1 p1)
	{
		VirtualInvokeData invokeData;
		il2cpp_codegen_get_generic_interface_invoke_data(method, obj, &invokeData);
		((Action)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};

struct Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C;
struct method_t81535E3A9A313D01BC63006B5A25610B21847FBD;
struct method_tF13E4C1D2723EFA21A0B8D9A21D42FE0334B238E;
struct CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB;
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
struct IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832;
struct SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C;
struct StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF;
struct Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07;
struct AudioBehaviour_t2DC0BEF7B020C952F3D2DA5AAAC88501C7EEB941;
struct AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20;
struct AudioResource_t35B84706031E4F08C928B1640B804839F4B6500A;
struct AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2;
struct AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299;
struct Behaviour_t01970CFBBA658497AE30F311C447DB0440BAB7FA;
struct ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67;
struct Delegate_t;
struct DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E;
struct IAudioGenerator_tD9DD08E5891ADC8BBD55CA6E1F00C0EED5C816DA;
struct IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220;
struct InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB;
struct MethodInfo_t;
struct NotImplementedException_t6366FE4DCF15094C51F4833B91A2AE68D4DA90E8;
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C;
struct ProcessorHeader_t2A43F64ECCCC872761CDBE0DA7A36DB193CA1085;
struct SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6;
struct String_t;
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;
struct PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E;
struct PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072;
struct SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30;
struct AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177;
struct Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060;

IL2CPP_EXTERN_C RuntimeClass* AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IAudioGenerator_tD9DD08E5891ADC8BBD55CA6E1F00C0EED5C816DA_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ICapabilities_tF49934ED8BF0930F82A0A54F794949FB61194793_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Mobile_t304A73480DF447472BDB16BA19A9E4FE2C8CB2DD_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* NotImplementedException_t6366FE4DCF15094C51F4833B91A2AE68D4DA90E8_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C String_t* _stringLiteral0D4DFFEDA70FA6D865B68C20C1F40F10FC31B6DD;
IL2CPP_EXTERN_C String_t* _stringLiteral23ABA34B45DF51C7D9C0E116AA6FDA4749E8C618;
IL2CPP_EXTERN_C String_t* _stringLiteral49403A17E8D32B35CB5B66AB1A2651A7EEAD1B00;
IL2CPP_EXTERN_C String_t* _stringLiteral5A10D0C56FEA8FB8816E3E1F6F88D55578F844F7;
IL2CPP_EXTERN_C String_t* _stringLiteral5C2C12AF4EFFBF824B747CFFBFC316CAE24077D1;
IL2CPP_EXTERN_C String_t* _stringLiteral66F9618FDA792CAB23AF2D7FFB50AB2D3E393DC5;
IL2CPP_EXTERN_C String_t* _stringLiteralC767BA736F2BEEB0EC1BA27ABE77214EC763C9B6;
IL2CPP_EXTERN_C String_t* _stringLiteralD5F2B3561CFA951FF37495C4B6AE6DB436EC2259;
IL2CPP_EXTERN_C String_t* _stringLiteralEE560D196E71D95F90A39910549B06428576F27C;
IL2CPP_EXTERN_C const RuntimeMethod* AudioClip_UnityEngine_Audio_GeneratorInstance_ICapabilities_get_isFinite_m20E61329E86505BA44C480115CD9A403FC63308C_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AudioClip_UnityEngine_Audio_GeneratorInstance_ICapabilities_get_isRealtime_mAC78FD95FB9998B38D0E50C2462DC9D9147FEAA4_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AudioClip_UnityEngine_Audio_GeneratorInstance_ICapabilities_get_length_mCA231C69B56E93689C2B45DCE2C3F0AD45CF541B_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AudioClip_UnityEngine_Audio_IAudioGenerator_CreateInstance_m78256DB80A6F6DB7AEF47DB8A3E827723D228FD5_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Handle_CheckValidOrThrow_mA6C3BAA711CDA462A6A2653A1A2D04CE2AECD3C3_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* MarshalledUnityObject_MarshalNotNull_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_mC3A4EC248FAFFBF10CD74B1E57A203FE7D9E01F5_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Nullable_1_GetValueOrDefault_m5879DF28E8CBC03BD7B2F3A7B383BB0068DE64DC_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Nullable_1__ctor_mA3535F58A54E060F67F4FFB3A5974A10D6444604_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Nullable_1_get_HasValue_m2C09C399366A16930BBC996441FE7CED2907D3D0_RuntimeMethod_var;
struct Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060;;
struct Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_com;
struct Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_com;;
struct Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_pinvoke;
struct Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_pinvoke;;
struct Delegate_t_marshaled_com;
struct Delegate_t_marshaled_pinvoke;
struct Exception_t_marshaled_com;
struct Exception_t_marshaled_pinvoke;

struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
struct SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C;

IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
struct U3CModuleU3E_t462BCCFB9B78348533823E0754F65F52A5348F89 
{
};
struct AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2  : public RuntimeObject
{
	SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* ___sampleFramesAvailable;
	SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* ___sampleFramesOverflow;
};
struct AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD  : public RuntimeObject
{
};
struct ScriptableGeneratorBindings_tB9FD8D788D5EDF66E708E851437C25CA5ECA514D  : public RuntimeObject
{
};
struct ScriptableProcessorBindings_t89BE2B31C684864ED0E1E5D6F978928182C9E9E8  : public RuntimeObject
{
};
struct String_t  : public RuntimeObject
{
	int32_t ____stringLength;
	Il2CppChar ____firstChar;
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F  : public RuntimeObject
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_pinvoke
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_com
{
};
struct Mobile_t304A73480DF447472BDB16BA19A9E4FE2C8CB2DD  : public RuntimeObject
{
};
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22 
{
	bool ___m_value;
};
struct DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E 
{
	int64_t ___Value;
};
struct Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F 
{
	double ___m_value;
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2  : public ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F
{
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_marshaled_pinvoke
{
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_marshaled_com
{
};
struct Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C 
{
	int32_t ___m_value;
};
struct Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3 
{
	int64_t ___m_value;
};
struct IntPtr_t 
{
	void* ___m_value;
};
struct Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C 
{
	float ___m_value;
};
struct UInt32_t1833D51FFA667B18A5AA4B8D34DE284F8495D29B 
{
	uint32_t ___m_value;
};
struct UInt64_t8F12534CC8FC4B5860F2A2CD1EE79D322E7A41AF 
{
	uint64_t ___m_value;
};
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915 
{
	union
	{
		struct
		{
		};
		uint8_t Void_t4861ACF8F4594C3437BB48B6E56783494B843915__padding[1];
	};
};
struct Properties_tA0A739E222CC4DF46C8E1F08B908950539845092 
{
	uint8_t ___m_Reserved;
};
struct Node_tE9DD997AC2A1CDC0DF9579D3B88467E7F42142C8 
{
	void* ___Next;
	int32_t ___Id;
	int32_t ___Version;
	int32_t ___AllocationFlags;
};
struct Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 
{
	bool ___hasValue;
	DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E ___value;
};
struct AudioSpeakerMode_tD681BDF379A4FB499C5562BF3F97AB93B4E540ED 
{
	int32_t ___value__;
};
struct ControlFunction_tF56532B76DBED412D41D1DC56F7002D533A56F87 
{
	uint32_t ___value__;
};
struct Delegate_t  : public RuntimeObject
{
	intptr_t ___method_ptr;
	intptr_t ___invoke_impl;
	RuntimeObject* ___m_target;
	intptr_t ___method;
	intptr_t ___delegate_trampoline;
	intptr_t ___extra_arg;
	intptr_t ___method_code;
	intptr_t ___interp_method;
	intptr_t ___interp_invoke_impl;
	MethodInfo_t* ___method_info;
	MethodInfo_t* ___original_method_info;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data;
	bool ___method_is_virtual;
};
struct Delegate_t_marshaled_pinvoke
{
	intptr_t ___method_ptr;
	intptr_t ___invoke_impl;
	Il2CppIUnknown* ___m_target;
	intptr_t ___method;
	intptr_t ___delegate_trampoline;
	intptr_t ___extra_arg;
	intptr_t ___method_code;
	intptr_t ___interp_method;
	intptr_t ___interp_invoke_impl;
	MethodInfo_t* ___method_info;
	MethodInfo_t* ___original_method_info;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data;
	int32_t ___method_is_virtual;
};
struct Delegate_t_marshaled_com
{
	intptr_t ___method_ptr;
	intptr_t ___invoke_impl;
	Il2CppIUnknown* ___m_target;
	intptr_t ___method;
	intptr_t ___delegate_trampoline;
	intptr_t ___extra_arg;
	intptr_t ___method_code;
	intptr_t ___interp_method;
	intptr_t ___interp_invoke_impl;
	MethodInfo_t* ___method_info;
	MethodInfo_t* ___original_method_info;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data;
	int32_t ___method_is_virtual;
};
struct Exception_t  : public RuntimeObject
{
	String_t* ____className;
	String_t* ____message;
	RuntimeObject* ____data;
	Exception_t* ____innerException;
	String_t* ____helpURL;
	RuntimeObject* ____stackTrace;
	String_t* ____stackTraceString;
	String_t* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	RuntimeObject* ____dynamicMethods;
	int32_t ____HResult;
	String_t* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct Exception_t_marshaled_pinvoke
{
	char* ____className;
	char* ____message;
	RuntimeObject* ____data;
	Exception_t_marshaled_pinvoke* ____innerException;
	char* ____helpURL;
	Il2CppIUnknown* ____stackTrace;
	char* ____stackTraceString;
	char* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	Il2CppIUnknown* ____dynamicMethods;
	int32_t ____HResult;
	char* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	Il2CppSafeArray* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct Exception_t_marshaled_com
{
	Il2CppChar* ____className;
	Il2CppChar* ____message;
	RuntimeObject* ____data;
	Exception_t_marshaled_com* ____innerException;
	Il2CppChar* ____helpURL;
	Il2CppIUnknown* ____stackTrace;
	Il2CppChar* ____stackTraceString;
	Il2CppChar* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	Il2CppIUnknown* ____dynamicMethods;
	int32_t ____HResult;
	Il2CppChar* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	Il2CppSafeArray* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC 
{
	intptr_t ___handle;
};
struct Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153 
{
	intptr_t ___m_Node;
	int32_t ___Version;
};
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C  : public RuntimeObject
{
	intptr_t ___m_CachedPtr;
};
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_pinvoke
{
	intptr_t ___m_CachedPtr;
};
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_com
{
	intptr_t ___m_CachedPtr;
};
struct PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 
{
	intptr_t ___m_Handle;
	uint32_t ___m_Version;
};
struct PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883 
{
	intptr_t ___m_Handle;
	uint32_t ___m_Version;
};
struct ProcessorFunction_t94187A2444D8312007E01AC295FBB4EFF25E4718 
{
	uint32_t ___value__;
};
struct Message_tBD40926CA51B245E5907EA3415980470455D8945 
{
	int64_t ___TypeHash;
	void* ___Data;
	intptr_t ___ManagedHandle;
};
struct UpdateSetting_tA679554B453B4685917A94E3D213100F94791EEE 
{
	int32_t ___value__;
};
struct AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0 
{
	PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 ___m_Handle;
};
struct AudioConfiguration_t4C8585EA98B6559B306B6FAD6A68088555394A5D 
{
	int32_t ___speakerMode;
	int32_t ___dspBufferSize;
	int32_t ___sampleRate;
	int32_t ___numRealVoices;
	int32_t ___numVirtualVoices;
};
struct AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C 
{
	PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 ___m_Handle;
};
struct AudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20 
{
	PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883 ___m_Handle;
};
struct AudioResource_t35B84706031E4F08C928B1640B804839F4B6500A  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};
struct Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};
struct ControlContext_t67F00D3C59C26C77BED313684AB258AEC5C5A243 
{
	ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67* ___m_Header;
	Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153 ___m_Handle;
};
struct ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67 
{
	Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153 ___Handle;
	intptr_t ___ManagedTransport;
};
struct MulticastDelegate_t  : public Delegate_t
{
	DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771* ___delegates;
};
struct MulticastDelegate_t_marshaled_pinvoke : public Delegate_t_marshaled_pinvoke
{
	Delegate_t_marshaled_pinvoke** ___delegates;
};
struct MulticastDelegate_t_marshaled_com : public Delegate_t_marshaled_com
{
	Delegate_t_marshaled_com** ___delegates;
};
struct ProcessorHeader_t2A43F64ECCCC872761CDBE0DA7A36DB193CA1085 
{
	void* ___m_Control;
	Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153 ___DualThreadHandle;
	void* ___NativeProcessorFunction;
	void* ___NativeControlFunction;
	intptr_t ___ProcessorReflectionData;
	intptr_t ___ControlReflectionData;
};
struct ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 
{
	Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153 ___Handle;
	ProcessorHeader_t2A43F64ECCCC872761CDBE0DA7A36DB193CA1085* ___Header;
};
struct SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295  : public Exception_t
{
};
struct Setup_t24BE50E09A2A2C409ED7EC3BCEB0B8ACD458A503 
{
	int32_t ___speakerMode;
	int32_t ___sampleRate;
};
struct CreationParameters_tC3AF64A7D0E7DAF87A587B34097DF91B377A9BD7 
{
	int32_t ___U3CcontrolUpdateSettingU3Ek__BackingField;
	int32_t ___U3CrealtimeUpdateSettingU3Ek__BackingField;
};
struct Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C  : public MulticastDelegate_t
{
};
struct Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07  : public MulticastDelegate_t
{
};
struct AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20  : public AudioResource_t35B84706031E4F08C928B1640B804839F4B6500A
{
	PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* ___m_PCMReaderCallback;
	PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* ___m_PCMSetPositionCallback;
};
struct AudioFormat_tC9651BFD36B41D1FC782C702B6C980D9768F699D 
{
	AudioConfiguration_t4C8585EA98B6559B306B6FAD6A68088555394A5D ___m_Config;
};
struct Behaviour_t01970CFBBA658497AE30F311C447DB0440BAB7FA  : public Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3
{
};
struct GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2 
{
	ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 ___m_ProcessorInstance;
};
struct InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
};
struct NotImplementedException_t6366FE4DCF15094C51F4833B91A2AE68D4DA90E8  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
};
struct PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E  : public MulticastDelegate_t
{
};
struct PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072  : public MulticastDelegate_t
{
};
struct SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30  : public MulticastDelegate_t
{
};
struct AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177  : public MulticastDelegate_t
{
};
struct Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060 
{
	Setup_t24BE50E09A2A2C409ED7EC3BCEB0B8ACD458A503 ___Setup;
	Properties_tA0A739E222CC4DF46C8E1F08B908950539845092 ___Properties;
	DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E ___ReportedLength;
	bool ___IsFinite;
	bool ___IsRealtime;
	bool ___HasKnownLength;
};
struct Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_pinvoke
{
	Setup_t24BE50E09A2A2C409ED7EC3BCEB0B8ACD458A503 ___Setup;
	Properties_tA0A739E222CC4DF46C8E1F08B908950539845092 ___Properties;
	DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E ___ReportedLength;
	int32_t ___IsFinite;
	int32_t ___IsRealtime;
	int32_t ___HasKnownLength;
};
struct Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_com
{
	Setup_t24BE50E09A2A2C409ED7EC3BCEB0B8ACD458A503 ___Setup;
	Properties_tA0A739E222CC4DF46C8E1F08B908950539845092 ___Properties;
	DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E ___ReportedLength;
	int32_t ___IsFinite;
	int32_t ___IsRealtime;
	int32_t ___HasKnownLength;
};
struct Nullable_1_t8743998EB2E7DB936C93772849EF306FBD5C5EE5 
{
	bool ___hasValue;
	AudioFormat_tC9651BFD36B41D1FC782C702B6C980D9768F699D ___value;
};
struct AudioBehaviour_t2DC0BEF7B020C952F3D2DA5AAAC88501C7EEB941  : public Behaviour_t01970CFBBA658497AE30F311C447DB0440BAB7FA
{
};
struct GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0 
{
	ProcessorHeader_t2A43F64ECCCC872761CDBE0DA7A36DB193CA1085 ___Processor;
	Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060 ___Configuration;
};
struct GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0_marshaled_pinvoke
{
	ProcessorHeader_t2A43F64ECCCC872761CDBE0DA7A36DB193CA1085 ___Processor;
	Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_pinvoke ___Configuration;
};
struct GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0_marshaled_com
{
	ProcessorHeader_t2A43F64ECCCC872761CDBE0DA7A36DB193CA1085 ___Processor;
	Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_com ___Configuration;
};
struct AudioListener_t1D629CE9BC079C8ECDE8F822616E8A8E319EAE35  : public AudioBehaviour_t2DC0BEF7B020C952F3D2DA5AAAC88501C7EEB941
{
};
struct AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299  : public AudioBehaviour_t2DC0BEF7B020C952F3D2DA5AAAC88501C7EEB941
{
};
struct AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_StaticFields
{
	AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177* ___OnAudioConfigurationChanged;
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___OnAudioSystemShuttingDown;
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___OnAudioSystemStartedUp;
};
struct String_t_StaticFields
{
	String_t* ___Empty;
};
struct Mobile_t304A73480DF447472BDB16BA19A9E4FE2C8CB2DD_StaticFields
{
	bool ___U3CmuteStateU3Ek__BackingField;
	bool ____stopAudioOutputOnMute;
	Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C* ___OnMuteStateChanged;
};
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_StaticFields
{
	String_t* ___TrueString;
	String_t* ___FalseString;
};
struct DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E_StaticFields
{
	DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E ___Zero;
	DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E ___MinValue;
	DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E ___MaxValue;
	int32_t ___TicksPerSecondBits;
	int32_t ___NonPow2TpsBits;
};
struct IntPtr_t_StaticFields
{
	intptr_t ___Zero;
};
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_StaticFields
{
	int32_t ___OffsetOfInstanceIDInCPlusPlusObject;
};
struct PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4_StaticFields
{
	PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 ___m_Null;
};
#ifdef __clang__
#pragma clang diagnostic pop
#endif
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771  : public RuntimeArray
{
	ALIGN_FIELD (8) Delegate_t* m_Items[1];

	inline Delegate_t* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline Delegate_t** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, Delegate_t* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline Delegate_t* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline Delegate_t** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, Delegate_t* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};
struct SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C  : public RuntimeArray
{
	ALIGN_FIELD (8) float m_Items[1];

	inline float GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline float* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, float value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline float GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline float* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, float value)
	{
		m_Items[index] = value;
	}
};

IL2CPP_EXTERN_C void Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshal_pinvoke(const Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060& unmarshaled, Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_pinvoke& marshaled);
IL2CPP_EXTERN_C void Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshal_pinvoke_back(const Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_pinvoke& marshaled, Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060& unmarshaled);
IL2CPP_EXTERN_C void Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshal_pinvoke_cleanup(Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_pinvoke& marshaled);
IL2CPP_EXTERN_C void Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshal_com(const Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060& unmarshaled, Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_com& marshaled);
IL2CPP_EXTERN_C void Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshal_com_back(const Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_com& marshaled, Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060& unmarshaled);
IL2CPP_EXTERN_C void Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshal_com_cleanup(Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_com& marshaled);

IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_m69C8773D6967F3B224777183E24EA621CE056F8F_gshared_inline (Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C* __this, bool ___0_obj, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR intptr_t MarshalledUnityObject_MarshalNotNull_TisRuntimeObject_mEB1AA6B672D00242BB9DCE007056EC0E9C8DB075_gshared_inline (RuntimeObject* ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Nullable_1__ctor_mA3535F58A54E060F67F4FFB3A5974A10D6444604_gshared (Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363* __this, DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E ___0_value, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Nullable_1_get_HasValue_m2C09C399366A16930BBC996441FE7CED2907D3D0_gshared_inline (Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E Nullable_1_GetValueOrDefault_m5879DF28E8CBC03BD7B2F3A7B383BB0068DE64DC_gshared_inline (Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363* __this, const RuntimeMethod* method) ;

IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AudioConfigurationChangeHandler_Invoke_m4DC27DD11512481B60071B20284E6886DAE54DE2_inline (AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177* __this, bool ___0_deviceWasChanged, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_Invoke_m7126A54DACA72B845424072887B5F3A51FC3808E_inline (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Mobile_get_muteState_m64C1E8C61537317A7F153E1A72F7D39D85DA684D_inline (const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Mobile_set_muteState_m7C9A464BCA3762330E18CCAD79AF6C47B863CA02_inline (bool ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Mobile_get_stopAudioOutputOnMute_m43EC82258D38C418353DFE19F32B51B64B18DCCA (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Mobile_StopAudioOutput_m10B8CEF668EE4967D0AD1D6741B6A37540C28A46 (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Mobile_StartAudioOutput_m731D1EEEE7A0D56BAADD571BA0FCAC13FB071223 (const RuntimeMethod* method) ;
inline void Action_1_Invoke_m69C8773D6967F3B224777183E24EA621CE056F8F_inline (Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C* __this, bool ___0_obj, const RuntimeMethod* method)
{
	((  void (*) (Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C*, bool, const RuntimeMethod*))Action_1_Invoke_m69C8773D6967F3B224777183E24EA621CE056F8F_gshared_inline)(__this, ___0_obj, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioSettings_StartAudioOutput_mB04D851DD0E6115DEEFB55779F880146263C67BE (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioSettings_StopAudioOutput_m3FE7A8EADAB2FB570BB05F7C353E25E15885D1CB (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioResource__ctor_m67FD564B101E07B1EA22EF27F3B5FA290A9D1409 (AudioResource_t35B84706031E4F08C928B1640B804839F4B6500A* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void PCMReaderCallback_Invoke_m76784C690C36B513E2AA5B0E4FD9831B2C7E5152_inline (PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* __this, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* ___0_data, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void PCMSetPositionCallback_Invoke_m434D4F02FA25F91DF6199EC5A799C551C7F93702_inline (PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* __this, int32_t ___0_position, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NotImplementedException__ctor_mDAB47BC6BD0E342E8F2171E5CABE3E67EA049F1C (NotImplementedException_t6366FE4DCF15094C51F4833B91A2AE68D4DA90E8* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Behaviour__ctor_m00422B6EFEA829BCB116D715E74F1EAD2CB6F4F8 (Behaviour_t01970CFBBA658497AE30F311C447DB0440BAB7FA* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_ThrowArgumentNullException_m57232D0804E4F65D1C0D86129C5BFD0DC950CA01 (RuntimeObject* ___0_obj, String_t* ___1_parameterName, const RuntimeMethod* method) ;
inline intptr_t MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_inline (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* ___0_obj, const RuntimeMethod* method)
{
	return ((  intptr_t (*) (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299*, const RuntimeMethod*))MarshalledUnityObject_MarshalNotNull_TisRuntimeObject_mEB1AA6B672D00242BB9DCE007056EC0E9C8DB075_gshared_inline)(___0_obj, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_PlayHelper_Injected_m05446E7631596FF4CEB719301AE808C954E7DFB3 (intptr_t ___0_source, uint64_t ___1_delay, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_ThrowNullReferenceException_mA9C7629D32240EE0218631933DAC647668CA63CF (RuntimeObject* ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_Play_Injected_m933CD7F5DD23F15817AB931871945E693A597C6C (intptr_t ___0__unity_self, double ___1_delay, const RuntimeMethod* method) ;
inline intptr_t MarshalledUnityObject_MarshalNotNull_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_mC3A4EC248FAFFBF10CD74B1E57A203FE7D9E01F5_inline (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___0_obj, const RuntimeMethod* method)
{
	return ((  intptr_t (*) (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20*, const RuntimeMethod*))MarshalledUnityObject_MarshalNotNull_TisRuntimeObject_mEB1AA6B672D00242BB9DCE007056EC0E9C8DB075_gshared_inline)(___0_obj, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_PlayOneShotHelper_Injected_mB8AC15832591B5B0DEE7E67B7E060618A23480F5 (intptr_t ___0_source, intptr_t ___1_clip, float ___2_volumeScale, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_PlayHelper_m4DE8C48925C3548BED306DAB9F87939F24A46960 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* ___0_source, uint64_t ___1_delay, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_PlayOneShot_mF6FE95C58996B38EF6E7F7482F95F5E15E0AB30B (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___0_clip, float ___1_volumeScale, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Object_op_Equality_mB6120F782D83091EF56A198FCEBCF066DB4A9605 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___0_x, Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___1_y, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9 (RuntimeObject* ___0_message, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_PlayOneShotHelper_mD110EAF42353687BD0B1190EEF30F0C65A4CF265 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* ___0_source, AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___1_clip, float ___2_volumeScale, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_loop_Injected_mEF9474AF2A1FC89B8DE5B578B167E1FE3C0E00CC (intptr_t ___0__unity_self, bool ___1_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_playOnAwake_Injected_m009A05A89DC8B7EDB36EA1F0CA4E49076993F9C9 (intptr_t ___0__unity_self, bool ___1_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioBehaviour__ctor_m6D88837496C42A746A51383F3D6F29CA72A9D309 (AudioBehaviour_t2DC0BEF7B020C952F3D2DA5AAAC88501C7EEB941* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_inline (SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* __this, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2* ___0_provider, uint32_t ___1_sampleFrameCount, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object__ctor_m2149FA40CEC8D82AC20D3508AB40C0D8EFEF68E6 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 AudioClipPlayable_GetHandle_mEA1D664328FF9B08E4F7D5EBCD4B51A754D97C44 (AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool PlayableHandle_op_Equality_m0E6C48A28F75A870AC22ADE3BD42F7F70A43C99C (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 ___0_x, PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 ___1_y, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioClipPlayable_Equals_m9C1C75ACBB74FE06AD02BE4643F6EB39413EFF83 (AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0* __this, AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0 ___0_other, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 AudioMixerPlayable_GetHandle_m6C182D9794E901D123223BB57738A302BEAB41FD (AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioMixerPlayable_Equals_mDFB945EB48199A338BAD00D40FB8EEC34CF64D57 (AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C* __this, AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C ___0_other, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Handle_CheckValidOrThrow_mA6C3BAA711CDA462A6A2653A1A2D04CE2AECD3C3 (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ControlContext__ctor_mFFC3A14E72F61DACB51F4F771FAE8416702023B6 (ControlContext_t67F00D3C59C26C77BED313684AB258AEC5C5A243* __this, void* ___0_headerThatShouldBeOfResourceType, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ScriptableProcessorBindings_CheckProcessorExists_mCA17BD1A2B9E776034C7B312CA810B3B739AA04E (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153 ___0_handle, ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67* ___1_control, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ControlContext_Exists_mEEB0CCB244CABE9EAD81EBC5E54CB6B50E18FA0F (ControlContext_t67F00D3C59C26C77BED313684AB258AEC5C5A243* __this, ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 ___0_processorInstance, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060 ControlContext_GetConfiguration_m5EF5175CD07C7E1661F6AC36ECEA52A293DB0249 (ControlContext_t67F00D3C59C26C77BED313684AB258AEC5C5A243* __this, GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2 ___0_generatorInstance, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool IntPtr_op_Inequality_m90EFC9C4CAD9A33E309F2DDF98EE4E1DD253637B_inline (intptr_t ___0_value1, intptr_t ___1_value2, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC GCHandle_FromIntPtr_mA7848A4285D007CADC52B6272DB243C8FDFD5FAC (intptr_t ___0_value, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool GCHandle_get_IsAllocated_m241908103D8D867E11CCAB73C918729825E86843_inline (GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void GCHandle_Free_m1320A260E487EB1EA6D95F9E54BFFCB5A4EF83A3 (GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ProcessorInstance_Equals_m4873E7D141484FCADC7F0F9C64542D57E0D02398 (ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35* __this, ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 ___0_other, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool GeneratorInstance_Equals_m1FEFD7862F181D69396710522A061AA1A5189FE1 (GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2* __this, GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2 ___0_other, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool GeneratorInstance_Equals_m7412F772B53B5052FF5070FB2BB2099A93270206 (GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ProcessorInstance_GetHashCode_m607D39F5BA947B94238348CF61431A9D8049F9B0 (ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t GeneratorInstance_GetHashCode_mBF9BB29BE3D212AAAEF53A02346FED4B0435C7D2 (GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Configuration_get_isRealtime_m99E988C200947A8277C144A84CB1755B5A0C2D78_inline (Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060* __this, const RuntimeMethod* method) ;
inline void Nullable_1__ctor_mA3535F58A54E060F67F4FFB3A5974A10D6444604 (Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363* __this, DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E ___0_value, const RuntimeMethod* method)
{
	((  void (*) (Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363*, DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E, const RuntimeMethod*))Nullable_1__ctor_mA3535F58A54E060F67F4FFB3A5974A10D6444604_gshared)(__this, ___0_value, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 Configuration_get_length_mB699EC1CF6B8A0955A60B55ABB22AD00A3F6BC3B (Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Handle_Equals_m5D8356E5FCA91FFFEACC910C738C37690EBA2E64 (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* __this, Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153 ___0_other, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ProcessorInstance_Equals_mFE0F0DA5B311AD6D34EABD73F61C4AE3014084ED (ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Handle_GetHashCode_m8DD5E2EE5197C1FF37CD2CF3B4FD892A9E299DFE (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 GeneratorInstance_op_Implicit_mB7A6E46130D3BAB42E00F4DD08155EF286A16167_inline (GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2* ___0_generatorInstance, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Format_mA0534D6E2AE4D67A6BD8D45B3321323930EB930C (String_t* ___0_format, RuntimeObject* ___1_arg0, RuntimeObject* ___2_arg1, RuntimeObject* ___3_arg2, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Debug_LogError_mB00B2B4468EF3CAF041B038D840820FB84C924B2 (RuntimeObject* ___0_message, const RuntimeMethod* method) ;
inline bool Nullable_1_get_HasValue_m2C09C399366A16930BBC996441FE7CED2907D3D0_inline (Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363*, const RuntimeMethod*))Nullable_1_get_HasValue_m2C09C399366A16930BBC996441FE7CED2907D3D0_gshared_inline)(__this, method);
}
inline DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E Nullable_1_GetValueOrDefault_m5879DF28E8CBC03BD7B2F3A7B383BB0068DE64DC_inline (Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363* __this, const RuntimeMethod* method)
{
	return ((  DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E (*) (Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363*, const RuntimeMethod*))Nullable_1_GetValueOrDefault_m5879DF28E8CBC03BD7B2F3A7B383BB0068DE64DC_gshared_inline)(__this, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool DiscreteTime_op_Inequality_m4C12F6A54E5C6E8BAC45A3903E6C2A3127C6063C_inline (DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E ___0_lhs, DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E ___1_rhs, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987 (String_t* ___0_format, RuntimeObject* ___1_arg0, RuntimeObject* ___2_arg1, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ScriptableProcessorBindings_CheckProcessorExistsInternal_mF6C33BA9D06B46EAAA47D05E74BA20CC604141A6 (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153 ___0_handle, void* ___1_control, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ScriptableProcessorBindings_CheckProcessorExistsInternal_Injected_mD29018C86FF29463FEC0346E4D8308F7F6EA1831 (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* ___0_handle, void* ___1_control, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void* IntPtr_op_Explicit_m2728CBA081E79B97DDCF1D4FAD77B309CA1E94BF (intptr_t ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Node_tE9DD997AC2A1CDC0DF9579D3B88467E7F42142C8* Handle_get_AtomicNode_m3D7AF7852BDF5999877F8F973FDF8724974B0A50 (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline (intptr_t ___0_value1, intptr_t ___1_value2, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Handle_Equals_mDE217E745B1016A74BCE82848675ED913B391B8F (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t IntPtr_op_Explicit_mC33775570B5AC86421DABA8F8B9885DBFF49B02F (intptr_t ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Handle_get_ValidAndNotDisposed_m1097F1E099632EDF74478AD5B74978BD3FAEF145 (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162 (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* __this, String_t* ___0_message, const RuntimeMethod* method) ;
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 87482
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSettings_InvokeOnAudioConfigurationChanged_m8273D3AEB24F4C3E374238B6F699BE6696808E85 (bool ___0_deviceWasChanged, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:412>
		AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177* L_0 = ((AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_StaticFields*)il2cpp_codegen_static_fields_for(AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_il2cpp_TypeInfo_var))->___OnAudioConfigurationChanged;
		V_0 = (bool)((!(((RuntimeObject*)(AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177*)L_0) <= ((RuntimeObject*)(RuntimeObject*)NULL)))? 1 : 0);
		bool L_1 = V_0;
		if (!L_1)
		{
			goto IL_0019;
		}
	}
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:413>
		AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177* L_2 = ((AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_StaticFields*)il2cpp_codegen_static_fields_for(AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_il2cpp_TypeInfo_var))->___OnAudioConfigurationChanged;
		bool L_3 = ___0_deviceWasChanged;
		NullCheck(L_2);
		AudioConfigurationChangeHandler_Invoke_m4DC27DD11512481B60071B20284E6886DAE54DE2_inline(L_2, L_3, NULL);
	}

IL_0019:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:414>
		return;
	}
}
// Method Definition Index: 87483
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSettings_InvokeOnAudioSystemShuttingDown_m1B9895D60B3267EBDEC69B9169730DBAD8325E90 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* G_B2_0 = NULL;
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* G_B1_0 = NULL;
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:418>
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_0 = ((AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_StaticFields*)il2cpp_codegen_static_fields_for(AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_il2cpp_TypeInfo_var))->___OnAudioSystemShuttingDown;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_1 = L_0;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000b;
		}
		G_B1_0 = L_1;
	}
	{
		goto IL_0011;
	}

IL_000b:
	{
		NullCheck(G_B2_0);
		Action_Invoke_m7126A54DACA72B845424072887B5F3A51FC3808E_inline(G_B2_0, NULL);
	}

IL_0011:
	{
		return;
	}
}
// Method Definition Index: 87484
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSettings_InvokeOnAudioSystemStartedUp_m7FE042936237E5BDCB20299D8C4CF583B661468C (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* G_B2_0 = NULL;
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* G_B1_0 = NULL;
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:422>
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_0 = ((AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_StaticFields*)il2cpp_codegen_static_fields_for(AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_il2cpp_TypeInfo_var))->___OnAudioSystemStartedUp;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_1 = L_0;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000b;
		}
		G_B1_0 = L_1;
	}
	{
		goto IL_0011;
	}

IL_000b:
	{
		NullCheck(G_B2_0);
		Action_Invoke_m7126A54DACA72B845424072887B5F3A51FC3808E_inline(G_B2_0, NULL);
	}

IL_0011:
	{
		return;
	}
}
// Method Definition Index: 87485
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioSettings_StartAudioOutput_mB04D851DD0E6115DEEFB55779F880146263C67BE (const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	typedef bool (*AudioSettings_StartAudioOutput_mB04D851DD0E6115DEEFB55779F880146263C67BE_ftn) ();
	static AudioSettings_StartAudioOutput_mB04D851DD0E6115DEEFB55779F880146263C67BE_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioSettings_StartAudioOutput_mB04D851DD0E6115DEEFB55779F880146263C67BE_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioSettings::StartAudioOutput()");
	bool icallRetVal = _il2cpp_icall_func();
	return icallRetVal;
}
// Method Definition Index: 87486
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioSettings_StopAudioOutput_m3FE7A8EADAB2FB570BB05F7C353E25E15885D1CB (const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	typedef bool (*AudioSettings_StopAudioOutput_m3FE7A8EADAB2FB570BB05F7C353E25E15885D1CB_ftn) ();
	static AudioSettings_StopAudioOutput_m3FE7A8EADAB2FB570BB05F7C353E25E15885D1CB_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioSettings_StopAudioOutput_m3FE7A8EADAB2FB570BB05F7C353E25E15885D1CB_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioSettings::StopAudioOutput()");
	bool icallRetVal = _il2cpp_icall_func();
	return icallRetVal;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
void AudioConfigurationChangeHandler_Invoke_m4DC27DD11512481B60071B20284E6886DAE54DE2_Multicast(AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177* __this, bool ___0_deviceWasChanged, const RuntimeMethod* method)
{
	il2cpp_array_size_t length = __this->___delegates->max_length;
	Delegate_t** delegatesToInvoke = reinterpret_cast<Delegate_t**>(__this->___delegates->GetAddressAtUnchecked(0));
	for (il2cpp_array_size_t i = 0; i < length; i++)
	{
		AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177* currentDelegate = reinterpret_cast<AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177*>(delegatesToInvoke[i]);
		typedef void (*FunctionPointerType) (RuntimeObject*, bool, const RuntimeMethod*);
		((FunctionPointerType)currentDelegate->___invoke_impl)((Il2CppObject*)currentDelegate->___method_code, ___0_deviceWasChanged, reinterpret_cast<RuntimeMethod*>(currentDelegate->___method));
	}
}
void AudioConfigurationChangeHandler_Invoke_m4DC27DD11512481B60071B20284E6886DAE54DE2_OpenInst(AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177* __this, bool ___0_deviceWasChanged, const RuntimeMethod* method)
{
	typedef void (*FunctionPointerType) (bool, const RuntimeMethod*);
	((FunctionPointerType)__this->___method_ptr)(___0_deviceWasChanged, method);
}
void AudioConfigurationChangeHandler_Invoke_m4DC27DD11512481B60071B20284E6886DAE54DE2_OpenStatic(AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177* __this, bool ___0_deviceWasChanged, const RuntimeMethod* method)
{
	typedef void (*FunctionPointerType) (bool, const RuntimeMethod*);
	((FunctionPointerType)__this->___method_ptr)(___0_deviceWasChanged, method);
}
IL2CPP_EXTERN_C  void DelegatePInvokeWrapper_AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177 (AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177* __this, bool ___0_deviceWasChanged, const RuntimeMethod* method)
{
	typedef void (DEFAULT_CALL *PInvokeFunc)(int32_t);
	PInvokeFunc il2cppPInvokeFunc = reinterpret_cast<PInvokeFunc>(il2cpp_codegen_get_reverse_pinvoke_function_ptr(__this));
	il2cppPInvokeFunc(static_cast<int32_t>(___0_deviceWasChanged));

}
// Method Definition Index: 87487
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioConfigurationChangeHandler__ctor_mA9827AB9472EC8EE0A0F0FC24EBC06B4740DD944 (AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	__this->___method_ptr = (intptr_t)il2cpp_codegen_get_method_pointer((RuntimeMethod*)___1_method);
	__this->___method = ___1_method;
	__this->___m_target = ___0_object;
	Il2CppCodeGenWriteBarrier((void**)(&__this->___m_target), (void*)___0_object);
	int parameterCount = il2cpp_codegen_method_parameter_count((RuntimeMethod*)___1_method);
	__this->___method_code = (intptr_t)__this;
	if (MethodIsStatic((RuntimeMethod*)___1_method))
	{
		bool isOpen = parameterCount == 1;
		if (isOpen)
			__this->___invoke_impl = (intptr_t)&AudioConfigurationChangeHandler_Invoke_m4DC27DD11512481B60071B20284E6886DAE54DE2_OpenStatic;
		else
			{
				__this->___invoke_impl = __this->___method_ptr;
				__this->___method_code = (intptr_t)__this->___m_target;
			}
	}
	else
	{
		if (___0_object == NULL)
			il2cpp_codegen_raise_exception(il2cpp_codegen_get_argument_exception(NULL, "Delegate to an instance method cannot have null 'this'."), NULL);
		__this->___invoke_impl = __this->___method_ptr;
		__this->___method_code = (intptr_t)__this->___m_target;
	}
	__this->___extra_arg = (intptr_t)&AudioConfigurationChangeHandler_Invoke_m4DC27DD11512481B60071B20284E6886DAE54DE2_Multicast;
}
// Method Definition Index: 87488
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioConfigurationChangeHandler_Invoke_m4DC27DD11512481B60071B20284E6886DAE54DE2 (AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177* __this, bool ___0_deviceWasChanged, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	typedef void (*FunctionPointerType) (RuntimeObject*, bool, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_deviceWasChanged, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 87489
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Mobile_get_muteState_m64C1E8C61537317A7F153E1A72F7D39D85DA684D (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mobile_t304A73480DF447472BDB16BA19A9E4FE2C8CB2DD_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:459>
		bool L_0 = ((Mobile_t304A73480DF447472BDB16BA19A9E4FE2C8CB2DD_StaticFields*)il2cpp_codegen_static_fields_for(Mobile_t304A73480DF447472BDB16BA19A9E4FE2C8CB2DD_il2cpp_TypeInfo_var))->___U3CmuteStateU3Ek__BackingField;
		return L_0;
	}
}
// Method Definition Index: 87490
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Mobile_set_muteState_m7C9A464BCA3762330E18CCAD79AF6C47B863CA02 (bool ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mobile_t304A73480DF447472BDB16BA19A9E4FE2C8CB2DD_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:460>
		bool L_0 = ___0_value;
		((Mobile_t304A73480DF447472BDB16BA19A9E4FE2C8CB2DD_StaticFields*)il2cpp_codegen_static_fields_for(Mobile_t304A73480DF447472BDB16BA19A9E4FE2C8CB2DD_il2cpp_TypeInfo_var))->___U3CmuteStateU3Ek__BackingField = L_0;
		return;
	}
}
// Method Definition Index: 87491
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Mobile_get_stopAudioOutputOnMute_m43EC82258D38C418353DFE19F32B51B64B18DCCA (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mobile_t304A73480DF447472BDB16BA19A9E4FE2C8CB2DD_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:468>
		bool L_0 = ((Mobile_t304A73480DF447472BDB16BA19A9E4FE2C8CB2DD_StaticFields*)il2cpp_codegen_static_fields_for(Mobile_t304A73480DF447472BDB16BA19A9E4FE2C8CB2DD_il2cpp_TypeInfo_var))->____stopAudioOutputOnMute;
		V_0 = L_0;
		goto IL_0009;
	}

IL_0009:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:469>
		bool L_1 = V_0;
		return L_1;
	}
}
// Method Definition Index: 87492
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Mobile_InvokeOnMuteStateChanged_mE5242862F948BA9FBB013A2B45F645B6A21E6198 (bool ___0_mute, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mobile_t304A73480DF447472BDB16BA19A9E4FE2C8CB2DD_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	bool V_1 = false;
	bool V_2 = false;
	bool V_3 = false;
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:493>
		bool L_0 = ___0_mute;
		bool L_1;
		L_1 = Mobile_get_muteState_m64C1E8C61537317A7F153E1A72F7D39D85DA684D_inline(NULL);
		V_0 = (bool)((((int32_t)((((int32_t)L_0) == ((int32_t)L_1))? 1 : 0)) == ((int32_t)0))? 1 : 0);
		bool L_2 = V_0;
		if (!L_2)
		{
			goto IL_0053;
		}
	}
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:495>
		bool L_3 = ___0_mute;
		Mobile_set_muteState_m7C9A464BCA3762330E18CCAD79AF6C47B863CA02_inline(L_3, NULL);
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:496>
		bool L_4;
		L_4 = Mobile_get_stopAudioOutputOnMute_m43EC82258D38C418353DFE19F32B51B64B18DCCA(NULL);
		V_1 = L_4;
		bool L_5 = V_1;
		if (!L_5)
		{
			goto IL_003a;
		}
	}
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:498>
		bool L_6;
		L_6 = Mobile_get_muteState_m64C1E8C61537317A7F153E1A72F7D39D85DA684D_inline(NULL);
		V_2 = L_6;
		bool L_7 = V_2;
		if (!L_7)
		{
			goto IL_0033;
		}
	}
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:499>
		Mobile_StopAudioOutput_m10B8CEF668EE4967D0AD1D6741B6A37540C28A46(NULL);
		goto IL_0039;
	}

IL_0033:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:501>
		Mobile_StartAudioOutput_m731D1EEEE7A0D56BAADD571BA0FCAC13FB071223(NULL);
	}

IL_0039:
	{
	}

IL_003a:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:503>
		Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C* L_8 = ((Mobile_t304A73480DF447472BDB16BA19A9E4FE2C8CB2DD_StaticFields*)il2cpp_codegen_static_fields_for(Mobile_t304A73480DF447472BDB16BA19A9E4FE2C8CB2DD_il2cpp_TypeInfo_var))->___OnMuteStateChanged;
		V_3 = (bool)((!(((RuntimeObject*)(Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C*)L_8) <= ((RuntimeObject*)(RuntimeObject*)NULL)))? 1 : 0);
		bool L_9 = V_3;
		if (!L_9)
		{
			goto IL_0052;
		}
	}
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:504>
		Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C* L_10 = ((Mobile_t304A73480DF447472BDB16BA19A9E4FE2C8CB2DD_StaticFields*)il2cpp_codegen_static_fields_for(Mobile_t304A73480DF447472BDB16BA19A9E4FE2C8CB2DD_il2cpp_TypeInfo_var))->___OnMuteStateChanged;
		bool L_11 = ___0_mute;
		NullCheck(L_10);
		Action_1_Invoke_m69C8773D6967F3B224777183E24EA621CE056F8F_inline(L_10, L_11, NULL);
	}

IL_0052:
	{
	}

IL_0053:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:506>
		return;
	}
}
// Method Definition Index: 87493
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Mobile_InvokeIsStopAudioOutputOnMuteEnabled_m854CB455C7BE7ADC06BABCB9AA24F60309AE7ED1 (const RuntimeMethod* method) 
{
	bool V_0 = false;
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:511>
		bool L_0;
		L_0 = Mobile_get_stopAudioOutputOnMute_m43EC82258D38C418353DFE19F32B51B64B18DCCA(NULL);
		V_0 = L_0;
		goto IL_0009;
	}

IL_0009:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:512>
		bool L_1 = V_0;
		return L_1;
	}
}
// Method Definition Index: 87494
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Mobile_StartAudioOutput_m731D1EEEE7A0D56BAADD571BA0FCAC13FB071223 (const RuntimeMethod* method) 
{
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:516>
		bool L_0;
		L_0 = AudioSettings_StartAudioOutput_mB04D851DD0E6115DEEFB55779F880146263C67BE(NULL);
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:517>
		return;
	}
}
// Method Definition Index: 87495
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Mobile_StopAudioOutput_m10B8CEF668EE4967D0AD1D6741B6A37540C28A46 (const RuntimeMethod* method) 
{
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:521>
		bool L_0;
		L_0 = AudioSettings_StopAudioOutput_m3FE7A8EADAB2FB570BB05F7C353E25E15885D1CB(NULL);
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:522>
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 87496
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioClip__ctor_m038DA97CB07076D1D9391E1E103F0F41D3622F89 (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, const RuntimeMethod* method) 
{
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:733>
		__this->___m_PCMReaderCallback = (PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_PCMReaderCallback), (void*)(PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E*)NULL);
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:737>
		__this->___m_PCMSetPositionCallback = (PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_PCMSetPositionCallback), (void*)(PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072*)NULL);
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:569>
		AudioResource__ctor_m67FD564B101E07B1EA22EF27F3B5FA290A9D1409(__this, NULL);
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:569>
		return;
	}
}
// Method Definition Index: 87497
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioClip_InvokePCMReaderCallback_Internal_m766E5705AB5AE16F5F142867CC3758ABE4BF462C (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* ___0_data, const RuntimeMethod* method) 
{
	bool V_0 = false;
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:742>
		PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* L_0 = __this->___m_PCMReaderCallback;
		V_0 = (bool)((!(((RuntimeObject*)(PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E*)L_0) <= ((RuntimeObject*)(RuntimeObject*)NULL)))? 1 : 0);
		bool L_1 = V_0;
		if (!L_1)
		{
			goto IL_001b;
		}
	}
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:743>
		PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* L_2 = __this->___m_PCMReaderCallback;
		SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* L_3 = ___0_data;
		NullCheck(L_2);
		PCMReaderCallback_Invoke_m76784C690C36B513E2AA5B0E4FD9831B2C7E5152_inline(L_2, L_3, NULL);
	}

IL_001b:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:744>
		return;
	}
}
// Method Definition Index: 87498
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioClip_InvokePCMSetPositionCallback_Internal_m986EF703B7DDE42343730DE93A095D05B9F4DBB8 (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, int32_t ___0_position, const RuntimeMethod* method) 
{
	bool V_0 = false;
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:749>
		PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* L_0 = __this->___m_PCMSetPositionCallback;
		V_0 = (bool)((!(((RuntimeObject*)(PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072*)L_0) <= ((RuntimeObject*)(RuntimeObject*)NULL)))? 1 : 0);
		bool L_1 = V_0;
		if (!L_1)
		{
			goto IL_001b;
		}
	}
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:750>
		PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* L_2 = __this->___m_PCMSetPositionCallback;
		int32_t L_3 = ___0_position;
		NullCheck(L_2);
		PCMSetPositionCallback_Invoke_m434D4F02FA25F91DF6199EC5A799C551C7F93702_inline(L_2, L_3, NULL);
	}

IL_001b:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:751>
		return;
	}
}
// Method Definition Index: 87499
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioClip_UnityEngine_Audio_GeneratorInstance_ICapabilities_get_isRealtime_mAC78FD95FB9998B38D0E50C2462DC9D9147FEAA4 (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, const RuntimeMethod* method) 
{
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:755>
		NotImplementedException_t6366FE4DCF15094C51F4833B91A2AE68D4DA90E8* L_0 = (NotImplementedException_t6366FE4DCF15094C51F4833B91A2AE68D4DA90E8*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotImplementedException_t6366FE4DCF15094C51F4833B91A2AE68D4DA90E8_il2cpp_TypeInfo_var)));
		NotImplementedException__ctor_mDAB47BC6BD0E342E8F2171E5CABE3E67EA049F1C(L_0, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AudioClip_UnityEngine_Audio_GeneratorInstance_ICapabilities_get_isRealtime_mAC78FD95FB9998B38D0E50C2462DC9D9147FEAA4_RuntimeMethod_var)));
	}
}
// Method Definition Index: 87500
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioClip_UnityEngine_Audio_GeneratorInstance_ICapabilities_get_isFinite_m20E61329E86505BA44C480115CD9A403FC63308C (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, const RuntimeMethod* method) 
{
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:756>
		NotImplementedException_t6366FE4DCF15094C51F4833B91A2AE68D4DA90E8* L_0 = (NotImplementedException_t6366FE4DCF15094C51F4833B91A2AE68D4DA90E8*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotImplementedException_t6366FE4DCF15094C51F4833B91A2AE68D4DA90E8_il2cpp_TypeInfo_var)));
		NotImplementedException__ctor_mDAB47BC6BD0E342E8F2171E5CABE3E67EA049F1C(L_0, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AudioClip_UnityEngine_Audio_GeneratorInstance_ICapabilities_get_isFinite_m20E61329E86505BA44C480115CD9A403FC63308C_RuntimeMethod_var)));
	}
}
// Method Definition Index: 87501
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 AudioClip_UnityEngine_Audio_GeneratorInstance_ICapabilities_get_length_mCA231C69B56E93689C2B45DCE2C3F0AD45CF541B (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, const RuntimeMethod* method) 
{
	Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:757>
		NotImplementedException_t6366FE4DCF15094C51F4833B91A2AE68D4DA90E8* L_0 = (NotImplementedException_t6366FE4DCF15094C51F4833B91A2AE68D4DA90E8*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotImplementedException_t6366FE4DCF15094C51F4833B91A2AE68D4DA90E8_il2cpp_TypeInfo_var)));
		NotImplementedException__ctor_mDAB47BC6BD0E342E8F2171E5CABE3E67EA049F1C(L_0, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AudioClip_UnityEngine_Audio_GeneratorInstance_ICapabilities_get_length_mCA231C69B56E93689C2B45DCE2C3F0AD45CF541B_RuntimeMethod_var)));
	}
}
// Method Definition Index: 87502
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2 AudioClip_UnityEngine_Audio_IAudioGenerator_CreateInstance_m78256DB80A6F6DB7AEF47DB8A3E827723D228FD5 (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, ControlContext_t67F00D3C59C26C77BED313684AB258AEC5C5A243 ___0_context, Nullable_1_t8743998EB2E7DB936C93772849EF306FBD5C5EE5 ___1_nestedFormat, CreationParameters_tC3AF64A7D0E7DAF87A587B34097DF91B377A9BD7 ___2_parameters, const RuntimeMethod* method) 
{
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:761>
		NotImplementedException_t6366FE4DCF15094C51F4833B91A2AE68D4DA90E8* L_0 = (NotImplementedException_t6366FE4DCF15094C51F4833B91A2AE68D4DA90E8*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotImplementedException_t6366FE4DCF15094C51F4833B91A2AE68D4DA90E8_il2cpp_TypeInfo_var)));
		NotImplementedException__ctor_mDAB47BC6BD0E342E8F2171E5CABE3E67EA049F1C(L_0, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AudioClip_UnityEngine_Audio_IAudioGenerator_CreateInstance_m78256DB80A6F6DB7AEF47DB8A3E827723D228FD5_RuntimeMethod_var)));
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
void PCMReaderCallback_Invoke_m76784C690C36B513E2AA5B0E4FD9831B2C7E5152_Multicast(PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* __this, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* ___0_data, const RuntimeMethod* method)
{
	il2cpp_array_size_t length = __this->___delegates->max_length;
	Delegate_t** delegatesToInvoke = reinterpret_cast<Delegate_t**>(__this->___delegates->GetAddressAtUnchecked(0));
	for (il2cpp_array_size_t i = 0; i < length; i++)
	{
		PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* currentDelegate = reinterpret_cast<PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E*>(delegatesToInvoke[i]);
		typedef void (*FunctionPointerType) (RuntimeObject*, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C*, const RuntimeMethod*);
		((FunctionPointerType)currentDelegate->___invoke_impl)((Il2CppObject*)currentDelegate->___method_code, ___0_data, reinterpret_cast<RuntimeMethod*>(currentDelegate->___method));
	}
}
void PCMReaderCallback_Invoke_m76784C690C36B513E2AA5B0E4FD9831B2C7E5152_OpenInst(PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* __this, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* ___0_data, const RuntimeMethod* method)
{
	NullCheck(___0_data);
	typedef void (*FunctionPointerType) (SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C*, const RuntimeMethod*);
	((FunctionPointerType)__this->___method_ptr)(___0_data, method);
}
void PCMReaderCallback_Invoke_m76784C690C36B513E2AA5B0E4FD9831B2C7E5152_OpenStatic(PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* __this, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* ___0_data, const RuntimeMethod* method)
{
	typedef void (*FunctionPointerType) (SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C*, const RuntimeMethod*);
	((FunctionPointerType)__this->___method_ptr)(___0_data, method);
}
IL2CPP_EXTERN_C  void DelegatePInvokeWrapper_PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E (PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* __this, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* ___0_data, const RuntimeMethod* method)
{
	typedef void (DEFAULT_CALL *PInvokeFunc)(float*);
	PInvokeFunc il2cppPInvokeFunc = reinterpret_cast<PInvokeFunc>(il2cpp_codegen_get_reverse_pinvoke_function_ptr(__this));
	float* ____0_data_marshaled = NULL;
	if (___0_data != NULL)
	{
		____0_data_marshaled = reinterpret_cast<float*>((___0_data)->GetAddressAtUnchecked(0));
	}

	il2cppPInvokeFunc(____0_data_marshaled);

}
// Method Definition Index: 87503
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PCMReaderCallback__ctor_mF621B6CC1A4BA6525190C5037401CF2FD5C0CF28 (PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	__this->___method_ptr = (intptr_t)il2cpp_codegen_get_method_pointer((RuntimeMethod*)___1_method);
	__this->___method = ___1_method;
	__this->___m_target = ___0_object;
	Il2CppCodeGenWriteBarrier((void**)(&__this->___m_target), (void*)___0_object);
	int parameterCount = il2cpp_codegen_method_parameter_count((RuntimeMethod*)___1_method);
	__this->___method_code = (intptr_t)__this;
	if (MethodIsStatic((RuntimeMethod*)___1_method))
	{
		bool isOpen = parameterCount == 1;
		if (isOpen)
			__this->___invoke_impl = (intptr_t)&PCMReaderCallback_Invoke_m76784C690C36B513E2AA5B0E4FD9831B2C7E5152_OpenStatic;
		else
			{
				__this->___invoke_impl = __this->___method_ptr;
				__this->___method_code = (intptr_t)__this->___m_target;
			}
	}
	else
	{
		bool isOpen = parameterCount == 0;
		if (isOpen)
		{
			__this->___invoke_impl = (intptr_t)&PCMReaderCallback_Invoke_m76784C690C36B513E2AA5B0E4FD9831B2C7E5152_OpenInst;
		}
		else
		{
			if (___0_object == NULL)
				il2cpp_codegen_raise_exception(il2cpp_codegen_get_argument_exception(NULL, "Delegate to an instance method cannot have null 'this'."), NULL);
			__this->___invoke_impl = __this->___method_ptr;
			__this->___method_code = (intptr_t)__this->___m_target;
		}
	}
	__this->___extra_arg = (intptr_t)&PCMReaderCallback_Invoke_m76784C690C36B513E2AA5B0E4FD9831B2C7E5152_Multicast;
}
// Method Definition Index: 87504
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PCMReaderCallback_Invoke_m76784C690C36B513E2AA5B0E4FD9831B2C7E5152 (PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* __this, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* ___0_data, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	typedef void (*FunctionPointerType) (RuntimeObject*, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C*, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_data, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
void PCMSetPositionCallback_Invoke_m434D4F02FA25F91DF6199EC5A799C551C7F93702_Multicast(PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* __this, int32_t ___0_position, const RuntimeMethod* method)
{
	il2cpp_array_size_t length = __this->___delegates->max_length;
	Delegate_t** delegatesToInvoke = reinterpret_cast<Delegate_t**>(__this->___delegates->GetAddressAtUnchecked(0));
	for (il2cpp_array_size_t i = 0; i < length; i++)
	{
		PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* currentDelegate = reinterpret_cast<PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072*>(delegatesToInvoke[i]);
		typedef void (*FunctionPointerType) (RuntimeObject*, int32_t, const RuntimeMethod*);
		((FunctionPointerType)currentDelegate->___invoke_impl)((Il2CppObject*)currentDelegate->___method_code, ___0_position, reinterpret_cast<RuntimeMethod*>(currentDelegate->___method));
	}
}
void PCMSetPositionCallback_Invoke_m434D4F02FA25F91DF6199EC5A799C551C7F93702_OpenInst(PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* __this, int32_t ___0_position, const RuntimeMethod* method)
{
	typedef void (*FunctionPointerType) (int32_t, const RuntimeMethod*);
	((FunctionPointerType)__this->___method_ptr)(___0_position, method);
}
void PCMSetPositionCallback_Invoke_m434D4F02FA25F91DF6199EC5A799C551C7F93702_OpenStatic(PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* __this, int32_t ___0_position, const RuntimeMethod* method)
{
	typedef void (*FunctionPointerType) (int32_t, const RuntimeMethod*);
	((FunctionPointerType)__this->___method_ptr)(___0_position, method);
}
IL2CPP_EXTERN_C  void DelegatePInvokeWrapper_PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072 (PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* __this, int32_t ___0_position, const RuntimeMethod* method)
{
	typedef void (DEFAULT_CALL *PInvokeFunc)(int32_t);
	PInvokeFunc il2cppPInvokeFunc = reinterpret_cast<PInvokeFunc>(il2cpp_codegen_get_reverse_pinvoke_function_ptr(__this));
	il2cppPInvokeFunc(___0_position);

}
// Method Definition Index: 87505
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PCMSetPositionCallback__ctor_mD16F77DDB552EB69BB3F5EF39420B2F09F95455B (PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	__this->___method_ptr = (intptr_t)il2cpp_codegen_get_method_pointer((RuntimeMethod*)___1_method);
	__this->___method = ___1_method;
	__this->___m_target = ___0_object;
	Il2CppCodeGenWriteBarrier((void**)(&__this->___m_target), (void*)___0_object);
	int parameterCount = il2cpp_codegen_method_parameter_count((RuntimeMethod*)___1_method);
	__this->___method_code = (intptr_t)__this;
	if (MethodIsStatic((RuntimeMethod*)___1_method))
	{
		bool isOpen = parameterCount == 1;
		if (isOpen)
			__this->___invoke_impl = (intptr_t)&PCMSetPositionCallback_Invoke_m434D4F02FA25F91DF6199EC5A799C551C7F93702_OpenStatic;
		else
			{
				__this->___invoke_impl = __this->___method_ptr;
				__this->___method_code = (intptr_t)__this->___m_target;
			}
	}
	else
	{
		if (___0_object == NULL)
			il2cpp_codegen_raise_exception(il2cpp_codegen_get_argument_exception(NULL, "Delegate to an instance method cannot have null 'this'."), NULL);
		__this->___invoke_impl = __this->___method_ptr;
		__this->___method_code = (intptr_t)__this->___m_target;
	}
	__this->___extra_arg = (intptr_t)&PCMSetPositionCallback_Invoke_m434D4F02FA25F91DF6199EC5A799C551C7F93702_Multicast;
}
// Method Definition Index: 87506
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PCMSetPositionCallback_Invoke_m434D4F02FA25F91DF6199EC5A799C551C7F93702 (PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* __this, int32_t ___0_position, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	typedef void (*FunctionPointerType) (RuntimeObject*, int32_t, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_position, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 87507
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioBehaviour__ctor_m6D88837496C42A746A51383F3D6F29CA72A9D309 (AudioBehaviour_t2DC0BEF7B020C952F3D2DA5AAAC88501C7EEB941* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Behaviour__ctor_m00422B6EFEA829BCB116D715E74F1EAD2CB6F4F8(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 87508
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_PlayHelper_m4DE8C48925C3548BED306DAB9F87939F24A46960 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* ___0_source, uint64_t ___1_delay, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral66F9618FDA792CAB23AF2D7FFB50AB2D3E393DC5);
		s_Il2CppMethodInitialized = true;
	}
	//<source_info:<no-source>:1>
	intptr_t G_B4_0;
	memset((&G_B4_0), 0, sizeof(G_B4_0));
	intptr_t G_B3_0;
	memset((&G_B3_0), 0, sizeof(G_B3_0));
	{
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_0 = ___0_source;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_1 = ___0_source;
		ThrowHelper_ThrowArgumentNullException_m57232D0804E4F65D1C0D86129C5BFD0DC950CA01(L_1, _stringLiteral66F9618FDA792CAB23AF2D7FFB50AB2D3E393DC5, NULL);
	}

IL_000e:
	{
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_2 = ___0_source;
		intptr_t L_3;
		L_3 = MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_inline(L_2, MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		intptr_t L_4 = L_3;
		if (L_4)
		{
			G_B4_0 = L_4;
			goto IL_0023;
		}
		G_B3_0 = L_4;
	}
	{
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_5 = ___0_source;
		ThrowHelper_ThrowArgumentNullException_m57232D0804E4F65D1C0D86129C5BFD0DC950CA01(L_5, _stringLiteral66F9618FDA792CAB23AF2D7FFB50AB2D3E393DC5, NULL);
		G_B4_0 = G_B3_0;
	}

IL_0023:
	{
		uint64_t L_6 = ___1_delay;
		AudioSource_PlayHelper_Injected_m05446E7631596FF4CEB719301AE808C954E7DFB3(G_B4_0, L_6, NULL);
		return;
	}
}
// Method Definition Index: 87509
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_Play_m10DB5ACD1CC32EE433DBC10416B1450A30DE5F16 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, double ___0_delay, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	//<source_info:<no-source>:1>
	intptr_t G_B2_0;
	memset((&G_B2_0), 0, sizeof(G_B2_0));
	intptr_t G_B1_0;
	memset((&G_B1_0), 0, sizeof(G_B1_0));
	{
		intptr_t L_0;
		L_0 = MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_inline(__this, MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		intptr_t L_1 = L_0;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000f;
		}
		G_B1_0 = L_1;
	}
	{
		ThrowHelper_ThrowNullReferenceException_mA9C7629D32240EE0218631933DAC647668CA63CF(__this, NULL);
		G_B2_0 = G_B1_0;
	}

IL_000f:
	{
		double L_2 = ___0_delay;
		AudioSource_Play_Injected_m933CD7F5DD23F15817AB931871945E693A597C6C(G_B2_0, L_2, NULL);
		return;
	}
}
// Method Definition Index: 87510
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_PlayOneShotHelper_mD110EAF42353687BD0B1190EEF30F0C65A4CF265 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* ___0_source, AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___1_clip, float ___2_volumeScale, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_MarshalNotNull_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_mC3A4EC248FAFFBF10CD74B1E57A203FE7D9E01F5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral66F9618FDA792CAB23AF2D7FFB50AB2D3E393DC5);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralD5F2B3561CFA951FF37495C4B6AE6DB436EC2259);
		s_Il2CppMethodInitialized = true;
	}
	//<source_info:<no-source>:1>
	intptr_t G_B6_0;
	memset((&G_B6_0), 0, sizeof(G_B6_0));
	intptr_t G_B5_0;
	memset((&G_B5_0), 0, sizeof(G_B5_0));
	intptr_t G_B8_0;
	memset((&G_B8_0), 0, sizeof(G_B8_0));
	intptr_t G_B8_1;
	memset((&G_B8_1), 0, sizeof(G_B8_1));
	intptr_t G_B7_0;
	memset((&G_B7_0), 0, sizeof(G_B7_0));
	intptr_t G_B7_1;
	memset((&G_B7_1), 0, sizeof(G_B7_1));
	{
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_0 = ___0_source;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_1 = ___0_source;
		ThrowHelper_ThrowArgumentNullException_m57232D0804E4F65D1C0D86129C5BFD0DC950CA01(L_1, _stringLiteral66F9618FDA792CAB23AF2D7FFB50AB2D3E393DC5, NULL);
	}

IL_000e:
	{
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_2 = ___1_clip;
		if (L_2)
		{
			goto IL_001d;
		}
	}
	{
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_3 = ___1_clip;
		ThrowHelper_ThrowArgumentNullException_m57232D0804E4F65D1C0D86129C5BFD0DC950CA01(L_3, _stringLiteralD5F2B3561CFA951FF37495C4B6AE6DB436EC2259, NULL);
	}

IL_001d:
	{
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_4 = ___0_source;
		intptr_t L_5;
		L_5 = MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_inline(L_4, MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		intptr_t L_6 = L_5;
		if (L_6)
		{
			G_B6_0 = L_6;
			goto IL_0032;
		}
		G_B5_0 = L_6;
	}
	{
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_7 = ___0_source;
		ThrowHelper_ThrowArgumentNullException_m57232D0804E4F65D1C0D86129C5BFD0DC950CA01(L_7, _stringLiteral66F9618FDA792CAB23AF2D7FFB50AB2D3E393DC5, NULL);
		G_B6_0 = G_B5_0;
	}

IL_0032:
	{
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_8 = ___1_clip;
		intptr_t L_9;
		L_9 = MarshalledUnityObject_MarshalNotNull_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_mC3A4EC248FAFFBF10CD74B1E57A203FE7D9E01F5_inline(L_8, MarshalledUnityObject_MarshalNotNull_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_mC3A4EC248FAFFBF10CD74B1E57A203FE7D9E01F5_RuntimeMethod_var);
		intptr_t L_10 = L_9;
		if (L_10)
		{
			G_B8_0 = L_10;
			G_B8_1 = G_B6_0;
			goto IL_0047;
		}
		G_B7_0 = L_10;
		G_B7_1 = G_B6_0;
	}
	{
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_11 = ___1_clip;
		ThrowHelper_ThrowArgumentNullException_m57232D0804E4F65D1C0D86129C5BFD0DC950CA01(L_11, _stringLiteralD5F2B3561CFA951FF37495C4B6AE6DB436EC2259, NULL);
		G_B8_0 = G_B7_0;
		G_B8_1 = G_B7_1;
	}

IL_0047:
	{
		float L_12 = ___2_volumeScale;
		AudioSource_PlayOneShotHelper_Injected_mB8AC15832591B5B0DEE7E67B7E060618A23480F5(G_B8_1, G_B8_0, L_12, NULL);
		return;
	}
}
// Method Definition Index: 87511
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_Play_m95DF07111C61D0E0F00257A00384D31531D590C3 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, const RuntimeMethod* method) 
{
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:997>
		AudioSource_PlayHelper_m4DE8C48925C3548BED306DAB9F87939F24A46960(__this, ((int64_t)0), NULL);
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:998>
		return;
	}
}
// Method Definition Index: 87512
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_Play_mC9D19FA54347ED102AD9913E3E7528BE969199FB (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, uint64_t ___0_delay, const RuntimeMethod* method) 
{
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:1002>
		uint64_t L_0 = ___0_delay;
		AudioSource_PlayHelper_m4DE8C48925C3548BED306DAB9F87939F24A46960(__this, L_0, NULL);
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:1003>
		return;
	}
}
// Method Definition Index: 87513
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_PlayOneShot_m098BCAE084AABB128BB19ED805D2D985E7B75112 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___0_clip, const RuntimeMethod* method) 
{
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:1021>
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_0 = ___0_clip;
		AudioSource_PlayOneShot_mF6FE95C58996B38EF6E7F7482F95F5E15E0AB30B(__this, L_0, (1.0f), NULL);
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:1022>
		return;
	}
}
// Method Definition Index: 87514
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_PlayOneShot_mF6FE95C58996B38EF6E7F7482F95F5E15E0AB30B (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___0_clip, float ___1_volumeScale, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral49403A17E8D32B35CB5B66AB1A2651A7EEAD1B00);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:1026>
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_0 = ___0_clip;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_1;
		L_1 = Object_op_Equality_mB6120F782D83091EF56A198FCEBCF066DB4A9605(L_0, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		V_0 = L_1;
		bool L_2 = V_0;
		if (!L_2)
		{
			goto IL_001a;
		}
	}
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:1028>
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(_stringLiteral49403A17E8D32B35CB5B66AB1A2651A7EEAD1B00, NULL);
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:1029>
		goto IL_0023;
	}

IL_001a:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:1032>
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_3 = ___0_clip;
		float L_4 = ___1_volumeScale;
		AudioSource_PlayOneShotHelper_mD110EAF42353687BD0B1190EEF30F0C65A4CF265(__this, L_3, L_4, NULL);
	}

IL_0023:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:1033>
		return;
	}
}
// Method Definition Index: 87515
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_loop_m834A590939D8456008C0F897FD80B0ECFFB7FE56 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, bool ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	//<source_info:<no-source>:1>
	intptr_t G_B2_0;
	memset((&G_B2_0), 0, sizeof(G_B2_0));
	intptr_t G_B1_0;
	memset((&G_B1_0), 0, sizeof(G_B1_0));
	{
		intptr_t L_0;
		L_0 = MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_inline(__this, MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		intptr_t L_1 = L_0;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000f;
		}
		G_B1_0 = L_1;
	}
	{
		ThrowHelper_ThrowNullReferenceException_mA9C7629D32240EE0218631933DAC647668CA63CF(__this, NULL);
		G_B2_0 = G_B1_0;
	}

IL_000f:
	{
		bool L_2 = ___0_value;
		AudioSource_set_loop_Injected_mEF9474AF2A1FC89B8DE5B578B167E1FE3C0E00CC(G_B2_0, L_2, NULL);
		return;
	}
}
// Method Definition Index: 87516
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_playOnAwake_m7EACC6ECEF12D7BA86A4E5A53603F1C8F9E11949 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, bool ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	//<source_info:<no-source>:1>
	intptr_t G_B2_0;
	memset((&G_B2_0), 0, sizeof(G_B2_0));
	intptr_t G_B1_0;
	memset((&G_B1_0), 0, sizeof(G_B1_0));
	{
		intptr_t L_0;
		L_0 = MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_inline(__this, MarshalledUnityObject_MarshalNotNull_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_mD9CAE1F7BCD08FDB40173EA86F253A6E87B8EF65_RuntimeMethod_var);
		intptr_t L_1 = L_0;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000f;
		}
		G_B1_0 = L_1;
	}
	{
		ThrowHelper_ThrowNullReferenceException_mA9C7629D32240EE0218631933DAC647668CA63CF(__this, NULL);
		G_B2_0 = G_B1_0;
	}

IL_000f:
	{
		bool L_2 = ___0_value;
		AudioSource_set_playOnAwake_Injected_m009A05A89DC8B7EDB36EA1F0CA4E49076993F9C9(G_B2_0, L_2, NULL);
		return;
	}
}
// Method Definition Index: 87517
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource__ctor_mC67BD65374AC3CDFB702307F4A89932D803191C1 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		AudioBehaviour__ctor_m6D88837496C42A746A51383F3D6F29CA72A9D309(__this, NULL);
		return;
	}
}
// Method Definition Index: 87518
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_PlayHelper_Injected_m05446E7631596FF4CEB719301AE808C954E7DFB3 (intptr_t ___0_source, uint64_t ___1_delay, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	typedef void (*AudioSource_PlayHelper_Injected_m05446E7631596FF4CEB719301AE808C954E7DFB3_ftn) (intptr_t, uint64_t);
	static AudioSource_PlayHelper_Injected_m05446E7631596FF4CEB719301AE808C954E7DFB3_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioSource_PlayHelper_Injected_m05446E7631596FF4CEB719301AE808C954E7DFB3_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioSource::PlayHelper_Injected(System.IntPtr,System.UInt64)");
	_il2cpp_icall_func(___0_source, ___1_delay);
}
// Method Definition Index: 87519
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_Play_Injected_m933CD7F5DD23F15817AB931871945E693A597C6C (intptr_t ___0__unity_self, double ___1_delay, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	typedef void (*AudioSource_Play_Injected_m933CD7F5DD23F15817AB931871945E693A597C6C_ftn) (intptr_t, double);
	static AudioSource_Play_Injected_m933CD7F5DD23F15817AB931871945E693A597C6C_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioSource_Play_Injected_m933CD7F5DD23F15817AB931871945E693A597C6C_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioSource::Play_Injected(System.IntPtr,System.Double)");
	_il2cpp_icall_func(___0__unity_self, ___1_delay);
}
// Method Definition Index: 87520
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_PlayOneShotHelper_Injected_mB8AC15832591B5B0DEE7E67B7E060618A23480F5 (intptr_t ___0_source, intptr_t ___1_clip, float ___2_volumeScale, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	typedef void (*AudioSource_PlayOneShotHelper_Injected_mB8AC15832591B5B0DEE7E67B7E060618A23480F5_ftn) (intptr_t, intptr_t, float);
	static AudioSource_PlayOneShotHelper_Injected_mB8AC15832591B5B0DEE7E67B7E060618A23480F5_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioSource_PlayOneShotHelper_Injected_mB8AC15832591B5B0DEE7E67B7E060618A23480F5_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioSource::PlayOneShotHelper_Injected(System.IntPtr,System.IntPtr,System.Single)");
	_il2cpp_icall_func(___0_source, ___1_clip, ___2_volumeScale);
}
// Method Definition Index: 87521
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_loop_Injected_mEF9474AF2A1FC89B8DE5B578B167E1FE3C0E00CC (intptr_t ___0__unity_self, bool ___1_value, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	typedef void (*AudioSource_set_loop_Injected_mEF9474AF2A1FC89B8DE5B578B167E1FE3C0E00CC_ftn) (intptr_t, bool);
	static AudioSource_set_loop_Injected_mEF9474AF2A1FC89B8DE5B578B167E1FE3C0E00CC_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioSource_set_loop_Injected_mEF9474AF2A1FC89B8DE5B578B167E1FE3C0E00CC_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioSource::set_loop_Injected(System.IntPtr,System.Boolean)");
	_il2cpp_icall_func(___0__unity_self, ___1_value);
}
// Method Definition Index: 87522
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_playOnAwake_Injected_m009A05A89DC8B7EDB36EA1F0CA4E49076993F9C9 (intptr_t ___0__unity_self, bool ___1_value, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	typedef void (*AudioSource_set_playOnAwake_Injected_m009A05A89DC8B7EDB36EA1F0CA4E49076993F9C9_ftn) (intptr_t, bool);
	static AudioSource_set_playOnAwake_Injected_m009A05A89DC8B7EDB36EA1F0CA4E49076993F9C9_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioSource_set_playOnAwake_Injected_m009A05A89DC8B7EDB36EA1F0CA4E49076993F9C9_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioSource::set_playOnAwake_Injected(System.IntPtr,System.Boolean)");
	_il2cpp_icall_func(___0__unity_self, ___1_value);
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 87523
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSampleProvider_InvokeSampleFramesAvailable_mEB16F7230AB65A3576BF053AC5719F8E134FBCD4 (AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2* __this, int32_t ___0_sampleFrameCount, const RuntimeMethod* method) 
{
	bool V_0 = false;
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/AudioSampleProvider.bindings.cs:178>
		SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* L_0 = __this->___sampleFramesAvailable;
		V_0 = (bool)((!(((RuntimeObject*)(SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30*)L_0) <= ((RuntimeObject*)(RuntimeObject*)NULL)))? 1 : 0);
		bool L_1 = V_0;
		if (!L_1)
		{
			goto IL_001c;
		}
	}
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/AudioSampleProvider.bindings.cs:180>
		SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* L_2 = __this->___sampleFramesAvailable;
		int32_t L_3 = ___0_sampleFrameCount;
		NullCheck(L_2);
		SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_inline(L_2, __this, L_3, NULL);
	}

IL_001c:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/AudioSampleProvider.bindings.cs:181>
		return;
	}
}
// Method Definition Index: 87524
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSampleProvider_InvokeSampleFramesOverflow_m66593173A527981F5EB2A5EF77B0C9119DAB5E15 (AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2* __this, int32_t ___0_droppedSampleFrameCount, const RuntimeMethod* method) 
{
	bool V_0 = false;
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/AudioSampleProvider.bindings.cs:186>
		SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* L_0 = __this->___sampleFramesOverflow;
		V_0 = (bool)((!(((RuntimeObject*)(SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30*)L_0) <= ((RuntimeObject*)(RuntimeObject*)NULL)))? 1 : 0);
		bool L_1 = V_0;
		if (!L_1)
		{
			goto IL_001c;
		}
	}
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/AudioSampleProvider.bindings.cs:187>
		SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* L_2 = __this->___sampleFramesOverflow;
		int32_t L_3 = ___0_droppedSampleFrameCount;
		NullCheck(L_2);
		SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_inline(L_2, __this, L_3, NULL);
	}

IL_001c:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/AudioSampleProvider.bindings.cs:188>
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
void SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_Multicast(SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* __this, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2* ___0_provider, uint32_t ___1_sampleFrameCount, const RuntimeMethod* method)
{
	il2cpp_array_size_t length = __this->___delegates->max_length;
	Delegate_t** delegatesToInvoke = reinterpret_cast<Delegate_t**>(__this->___delegates->GetAddressAtUnchecked(0));
	for (il2cpp_array_size_t i = 0; i < length; i++)
	{
		SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* currentDelegate = reinterpret_cast<SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30*>(delegatesToInvoke[i]);
		typedef void (*FunctionPointerType) (RuntimeObject*, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2*, uint32_t, const RuntimeMethod*);
		((FunctionPointerType)currentDelegate->___invoke_impl)((Il2CppObject*)currentDelegate->___method_code, ___0_provider, ___1_sampleFrameCount, reinterpret_cast<RuntimeMethod*>(currentDelegate->___method));
	}
}
void SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_OpenInst(SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* __this, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2* ___0_provider, uint32_t ___1_sampleFrameCount, const RuntimeMethod* method)
{
	NullCheck(___0_provider);
	typedef void (*FunctionPointerType) (AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2*, uint32_t, const RuntimeMethod*);
	((FunctionPointerType)__this->___method_ptr)(___0_provider, ___1_sampleFrameCount, method);
}
void SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_OpenStatic(SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* __this, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2* ___0_provider, uint32_t ___1_sampleFrameCount, const RuntimeMethod* method)
{
	typedef void (*FunctionPointerType) (AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2*, uint32_t, const RuntimeMethod*);
	((FunctionPointerType)__this->___method_ptr)(___0_provider, ___1_sampleFrameCount, method);
}
void SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_OpenVirtual(SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* __this, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2* ___0_provider, uint32_t ___1_sampleFrameCount, const RuntimeMethod* method)
{
	NullCheck(___0_provider);
	VirtualActionInvoker1< uint32_t >::Invoke(il2cpp_codegen_method_get_slot(method), ___0_provider, ___1_sampleFrameCount);
}
void SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_OpenInterface(SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* __this, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2* ___0_provider, uint32_t ___1_sampleFrameCount, const RuntimeMethod* method)
{
	NullCheck(___0_provider);
	InterfaceActionInvoker1< uint32_t >::Invoke(il2cpp_codegen_method_get_slot(method), il2cpp_codegen_method_get_declaring_type(method), ___0_provider, ___1_sampleFrameCount);
}
void SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_OpenGenericVirtual(SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* __this, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2* ___0_provider, uint32_t ___1_sampleFrameCount, const RuntimeMethod* method)
{
	NullCheck(___0_provider);
	GenericVirtualActionInvoker1< uint32_t >::Invoke(method, ___0_provider, ___1_sampleFrameCount);
}
void SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_OpenGenericInterface(SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* __this, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2* ___0_provider, uint32_t ___1_sampleFrameCount, const RuntimeMethod* method)
{
	NullCheck(___0_provider);
	GenericInterfaceActionInvoker1< uint32_t >::Invoke(method, ___0_provider, ___1_sampleFrameCount);
}
// Method Definition Index: 87525
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SampleFramesHandler__ctor_m7DDE0BAD439CD80791140C7D42D661B598A7663A (SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	__this->___method_ptr = (intptr_t)il2cpp_codegen_get_method_pointer((RuntimeMethod*)___1_method);
	__this->___method = ___1_method;
	__this->___m_target = ___0_object;
	Il2CppCodeGenWriteBarrier((void**)(&__this->___m_target), (void*)___0_object);
	int parameterCount = il2cpp_codegen_method_parameter_count((RuntimeMethod*)___1_method);
	__this->___method_code = (intptr_t)__this;
	if (MethodIsStatic((RuntimeMethod*)___1_method))
	{
		bool isOpen = parameterCount == 2;
		if (isOpen)
			__this->___invoke_impl = (intptr_t)&SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_OpenStatic;
		else
			{
				__this->___invoke_impl = __this->___method_ptr;
				__this->___method_code = (intptr_t)__this->___m_target;
			}
	}
	else
	{
		bool isOpen = parameterCount == 1;
		if (isOpen)
		{
			if (__this->___method_is_virtual)
			{
				if (il2cpp_codegen_method_is_generic_instance_method((RuntimeMethod*)___1_method))
					if (il2cpp_codegen_method_is_interface_method((RuntimeMethod*)___1_method))
						__this->___invoke_impl = (intptr_t)&SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_OpenGenericInterface;
					else
						__this->___invoke_impl = (intptr_t)&SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_OpenGenericVirtual;
				else
					if (il2cpp_codegen_method_is_interface_method((RuntimeMethod*)___1_method))
						__this->___invoke_impl = (intptr_t)&SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_OpenInterface;
					else
						__this->___invoke_impl = (intptr_t)&SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_OpenVirtual;
			}
			else
			{
				__this->___invoke_impl = (intptr_t)&SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_OpenInst;
			}
		}
		else
		{
			if (___0_object == NULL)
				il2cpp_codegen_raise_exception(il2cpp_codegen_get_argument_exception(NULL, "Delegate to an instance method cannot have null 'this'."), NULL);
			__this->___invoke_impl = __this->___method_ptr;
			__this->___method_code = (intptr_t)__this->___m_target;
		}
	}
	__this->___extra_arg = (intptr_t)&SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_Multicast;
}
// Method Definition Index: 87526
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC (SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* __this, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2* ___0_provider, uint32_t ___1_sampleFrameCount, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	typedef void (*FunctionPointerType) (RuntimeObject*, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2*, uint32_t, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_provider, ___1_sampleFrameCount, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 87527
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioResource__ctor_m67FD564B101E07B1EA22EF27F3B5FA290A9D1409 (AudioResource_t35B84706031E4F08C928B1640B804839F4B6500A* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:19>
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		Object__ctor_m2149FA40CEC8D82AC20D3508AB40C0D8EFEF68E6(__this, NULL);
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:19>
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 87528
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 AudioClipPlayable_GetHandle_mEA1D664328FF9B08E4F7D5EBCD4B51A754D97C44 (AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0* __this, const RuntimeMethod* method) 
{
	PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/AudioClipPlayable.bindings.cs:50>
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_0 = __this->___m_Handle;
		V_0 = L_0;
		goto IL_000a;
	}

IL_000a:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/AudioClipPlayable.bindings.cs:51>
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_1 = V_0;
		return L_1;
	}
}
IL2CPP_EXTERN_C  PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 AudioClipPlayable_GetHandle_mEA1D664328FF9B08E4F7D5EBCD4B51A754D97C44_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0*>(__this + _offset);
	PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 _returnValue;
	_returnValue = AudioClipPlayable_GetHandle_mEA1D664328FF9B08E4F7D5EBCD4B51A754D97C44(_thisAdjusted, method);
	return _returnValue;
}
// Method Definition Index: 87529
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioClipPlayable_Equals_m9C1C75ACBB74FE06AD02BE4643F6EB39413EFF83 (AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0* __this, AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0 ___0_other, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/AudioClipPlayable.bindings.cs:65>
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_0;
		L_0 = AudioClipPlayable_GetHandle_mEA1D664328FF9B08E4F7D5EBCD4B51A754D97C44(__this, NULL);
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_1;
		L_1 = AudioClipPlayable_GetHandle_mEA1D664328FF9B08E4F7D5EBCD4B51A754D97C44((&___0_other), NULL);
		il2cpp_codegen_runtime_class_init_inline(PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4_il2cpp_TypeInfo_var);
		bool L_2;
		L_2 = PlayableHandle_op_Equality_m0E6C48A28F75A870AC22ADE3BD42F7F70A43C99C(L_0, L_1, NULL);
		V_0 = L_2;
		goto IL_0016;
	}

IL_0016:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/AudioClipPlayable.bindings.cs:66>
		bool L_3 = V_0;
		return L_3;
	}
}
IL2CPP_EXTERN_C  bool AudioClipPlayable_Equals_m9C1C75ACBB74FE06AD02BE4643F6EB39413EFF83_AdjustorThunk (RuntimeObject* __this, AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0 ___0_other, const RuntimeMethod* method)
{
	AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0*>(__this + _offset);
	bool _returnValue;
	_returnValue = AudioClipPlayable_Equals_m9C1C75ACBB74FE06AD02BE4643F6EB39413EFF83(_thisAdjusted, ___0_other, method);
	return _returnValue;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 87530
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 AudioMixerPlayable_GetHandle_m6C182D9794E901D123223BB57738A302BEAB41FD (AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C* __this, const RuntimeMethod* method) 
{
	PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/AudioMixerPlayable.bindings.cs:48>
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_0 = __this->___m_Handle;
		V_0 = L_0;
		goto IL_000a;
	}

IL_000a:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/AudioMixerPlayable.bindings.cs:49>
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_1 = V_0;
		return L_1;
	}
}
IL2CPP_EXTERN_C  PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 AudioMixerPlayable_GetHandle_m6C182D9794E901D123223BB57738A302BEAB41FD_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C*>(__this + _offset);
	PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 _returnValue;
	_returnValue = AudioMixerPlayable_GetHandle_m6C182D9794E901D123223BB57738A302BEAB41FD(_thisAdjusted, method);
	return _returnValue;
}
// Method Definition Index: 87531
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioMixerPlayable_Equals_mDFB945EB48199A338BAD00D40FB8EEC34CF64D57 (AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C* __this, AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C ___0_other, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/AudioMixerPlayable.bindings.cs:63>
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_0;
		L_0 = AudioMixerPlayable_GetHandle_m6C182D9794E901D123223BB57738A302BEAB41FD(__this, NULL);
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_1;
		L_1 = AudioMixerPlayable_GetHandle_m6C182D9794E901D123223BB57738A302BEAB41FD((&___0_other), NULL);
		il2cpp_codegen_runtime_class_init_inline(PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4_il2cpp_TypeInfo_var);
		bool L_2;
		L_2 = PlayableHandle_op_Equality_m0E6C48A28F75A870AC22ADE3BD42F7F70A43C99C(L_0, L_1, NULL);
		V_0 = L_2;
		goto IL_0016;
	}

IL_0016:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/AudioMixerPlayable.bindings.cs:64>
		bool L_3 = V_0;
		return L_3;
	}
}
IL2CPP_EXTERN_C  bool AudioMixerPlayable_Equals_mDFB945EB48199A338BAD00D40FB8EEC34CF64D57_AdjustorThunk (RuntimeObject* __this, AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C ___0_other, const RuntimeMethod* method)
{
	AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C*>(__this + _offset);
	bool _returnValue;
	_returnValue = AudioMixerPlayable_Equals_mDFB945EB48199A338BAD00D40FB8EEC34CF64D57(_thisAdjusted, ___0_other, method);
	return _returnValue;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 87532
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ControlContext__ctor_mFFC3A14E72F61DACB51F4F771FAE8416702023B6 (ControlContext_t67F00D3C59C26C77BED313684AB258AEC5C5A243* __this, void* ___0_headerThatShouldBeOfResourceType, const RuntimeMethod* method) 
{
	ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67* V_0 = NULL;
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ControlContext.cs:191>
		void* L_0 = ___0_headerThatShouldBeOfResourceType;
		V_0 = (ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67*)L_0;
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ControlContext.cs:192>
		ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67* L_1 = V_0;
		NullCheck(L_1);
		Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153 L_2 = L_1->___Handle;
		__this->___m_Handle = L_2;
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ControlContext.cs:193>
		Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* L_3 = (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153*)(&__this->___m_Handle);
		Handle_CheckValidOrThrow_mA6C3BAA711CDA462A6A2653A1A2D04CE2AECD3C3(L_3, NULL);
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ControlContext.cs:195>
		ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67* L_4 = V_0;
		__this->___m_Header = L_4;
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ControlContext.cs:196>
		return;
	}
}
IL2CPP_EXTERN_C  void ControlContext__ctor_mFFC3A14E72F61DACB51F4F771FAE8416702023B6_AdjustorThunk (RuntimeObject* __this, void* ___0_headerThatShouldBeOfResourceType, const RuntimeMethod* method)
{
	ControlContext_t67F00D3C59C26C77BED313684AB258AEC5C5A243* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<ControlContext_t67F00D3C59C26C77BED313684AB258AEC5C5A243*>(__this + _offset);
	ControlContext__ctor_mFFC3A14E72F61DACB51F4F771FAE8416702023B6(_thisAdjusted, ___0_headerThatShouldBeOfResourceType, method);
}
// Method Definition Index: 87533
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ControlContext_Exists_mEEB0CCB244CABE9EAD81EBC5E54CB6B50E18FA0F (ControlContext_t67F00D3C59C26C77BED313684AB258AEC5C5A243* __this, ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 ___0_processorInstance, const RuntimeMethod* method) 
{
	bool V_0 = false;
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ControlContext.cs:323>
		Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* L_0 = (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153*)(&__this->___m_Handle);
		Handle_CheckValidOrThrow_mA6C3BAA711CDA462A6A2653A1A2D04CE2AECD3C3(L_0, NULL);
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ControlContext.cs:324>
		ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 L_1 = ___0_processorInstance;
		Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153 L_2 = L_1.___Handle;
		ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67* L_3 = __this->___m_Header;
		bool L_4;
		L_4 = ScriptableProcessorBindings_CheckProcessorExists_mCA17BD1A2B9E776034C7B312CA810B3B739AA04E(L_2, L_3, NULL);
		V_0 = L_4;
		goto IL_0021;
	}

IL_0021:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ControlContext.cs:325>
		bool L_5 = V_0;
		return L_5;
	}
}
IL2CPP_EXTERN_C  bool ControlContext_Exists_mEEB0CCB244CABE9EAD81EBC5E54CB6B50E18FA0F_AdjustorThunk (RuntimeObject* __this, ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 ___0_processorInstance, const RuntimeMethod* method)
{
	ControlContext_t67F00D3C59C26C77BED313684AB258AEC5C5A243* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<ControlContext_t67F00D3C59C26C77BED313684AB258AEC5C5A243*>(__this + _offset);
	bool _returnValue;
	_returnValue = ControlContext_Exists_mEEB0CCB244CABE9EAD81EBC5E54CB6B50E18FA0F(_thisAdjusted, ___0_processorInstance, method);
	return _returnValue;
}
// Method Definition Index: 87534
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060 ControlContext_GetConfiguration_m5EF5175CD07C7E1661F6AC36ECEA52A293DB0249 (ControlContext_t67F00D3C59C26C77BED313684AB258AEC5C5A243* __this, GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2 ___0_generatorInstance, const RuntimeMethod* method) 
{
	Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ControlContext.cs:408>
		Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* L_0 = (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153*)(&__this->___m_Handle);
		Handle_CheckValidOrThrow_mA6C3BAA711CDA462A6A2653A1A2D04CE2AECD3C3(L_0, NULL);
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ControlContext.cs:410>
		ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35* L_1 = (ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35*)(&(&___0_generatorInstance)->___m_ProcessorInstance);
		Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* L_2 = (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153*)(&L_1->___Handle);
		Handle_CheckValidOrThrow_mA6C3BAA711CDA462A6A2653A1A2D04CE2AECD3C3(L_2, NULL);
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ControlContext.cs:412>
		GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2 L_3 = ___0_generatorInstance;
		ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 L_4 = L_3.___m_ProcessorInstance;
		ProcessorHeader_t2A43F64ECCCC872761CDBE0DA7A36DB193CA1085* L_5 = L_4.___Header;
		NullCheck(L_5);
		Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060 L_6 = ((GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0*)L_5)->___Configuration;
		V_0 = L_6;
		goto IL_0032;
	}

IL_0032:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ControlContext.cs:413>
		Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060 L_7 = V_0;
		return L_7;
	}
}
IL2CPP_EXTERN_C  Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060 ControlContext_GetConfiguration_m5EF5175CD07C7E1661F6AC36ECEA52A293DB0249_AdjustorThunk (RuntimeObject* __this, GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2 ___0_generatorInstance, const RuntimeMethod* method)
{
	ControlContext_t67F00D3C59C26C77BED313684AB258AEC5C5A243* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<ControlContext_t67F00D3C59C26C77BED313684AB258AEC5C5A243*>(__this + _offset);
	Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060 _returnValue;
	_returnValue = ControlContext_GetConfiguration_m5EF5175CD07C7E1661F6AC36ECEA52A293DB0249(_thisAdjusted, ___0_generatorInstance, method);
	return _returnValue;
}
// Method Definition Index: 87535
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ControlContext_CleanupHeader_mC560997117E3E82A193438753C1020A76A208F5E (ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67* ___0_header, const RuntimeMethod* method) 
{
	bool V_0 = false;
	GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC V_1;
	memset((&V_1), 0, sizeof(V_1));
	bool V_2 = false;
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ControlContext.cs:547>
		ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67* L_0 = ___0_header;
		intptr_t L_1 = L_0->___ManagedTransport;
		bool L_2;
		L_2 = IntPtr_op_Inequality_m90EFC9C4CAD9A33E309F2DDF98EE4E1DD253637B_inline(L_1, 0, NULL);
		V_0 = L_2;
		bool L_3 = V_0;
		if (!L_3)
		{
			goto IL_0038;
		}
	}
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ControlContext.cs:550>
		ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67* L_4 = ___0_header;
		intptr_t L_5 = L_4->___ManagedTransport;
		GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC L_6;
		L_6 = GCHandle_FromIntPtr_mA7848A4285D007CADC52B6272DB243C8FDFD5FAC(L_5, NULL);
		V_1 = L_6;
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ControlContext.cs:551>
		bool L_7;
		L_7 = GCHandle_get_IsAllocated_m241908103D8D867E11CCAB73C918729825E86843_inline((&V_1), NULL);
		V_2 = L_7;
		bool L_8 = V_2;
		if (!L_8)
		{
			goto IL_0037;
		}
	}
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ControlContext.cs:553>
		GCHandle_Free_m1320A260E487EB1EA6D95F9E54BFFCB5A4EF83A3((&V_1), NULL);
	}

IL_0037:
	{
	}

IL_0038:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ControlContext.cs:556>
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 87536
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 GeneratorInstance_op_Implicit_mB7A6E46130D3BAB42E00F4DD08155EF286A16167 (GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2* ___0_generatorInstance, const RuntimeMethod* method) 
{
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ScriptableGenerator.bindings.cs:392>
		GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2* L_0 = ___0_generatorInstance;
		ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 L_1 = L_0->___m_ProcessorInstance;
		return L_1;
	}
}
// Method Definition Index: 87537
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool GeneratorInstance_Equals_m1FEFD7862F181D69396710522A061AA1A5189FE1 (GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2* __this, GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2 ___0_other, const RuntimeMethod* method) 
{
	bool V_0 = false;
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ScriptableGenerator.bindings.cs:401>
		ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35* L_0 = (ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35*)(&__this->___m_ProcessorInstance);
		GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2 L_1 = ___0_other;
		ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 L_2 = L_1.___m_ProcessorInstance;
		bool L_3;
		L_3 = ProcessorInstance_Equals_m4873E7D141484FCADC7F0F9C64542D57E0D02398(L_0, L_2, NULL);
		V_0 = L_3;
		goto IL_0015;
	}

IL_0015:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ScriptableGenerator.bindings.cs:402>
		bool L_4 = V_0;
		return L_4;
	}
}
IL2CPP_EXTERN_C  bool GeneratorInstance_Equals_m1FEFD7862F181D69396710522A061AA1A5189FE1_AdjustorThunk (RuntimeObject* __this, GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2 ___0_other, const RuntimeMethod* method)
{
	GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2*>(__this + _offset);
	bool _returnValue;
	_returnValue = GeneratorInstance_Equals_m1FEFD7862F181D69396710522A061AA1A5189FE1(_thisAdjusted, ___0_other, method);
	return _returnValue;
}
// Method Definition Index: 87538
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool GeneratorInstance_Equals_m7412F772B53B5052FF5070FB2BB2099A93270206 (GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	bool V_1 = false;
	bool V_2 = false;
	int32_t G_B5_0 = 0;
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ScriptableGenerator.bindings.cs:411>
		RuntimeObject* L_0 = ___0_obj;
		V_1 = (bool)((((RuntimeObject*)(RuntimeObject*)L_0) == ((RuntimeObject*)(RuntimeObject*)NULL))? 1 : 0);
		bool L_1 = V_1;
		if (!L_1)
		{
			goto IL_000d;
		}
	}
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ScriptableGenerator.bindings.cs:412>
		V_2 = (bool)0;
		goto IL_0029;
	}

IL_000d:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ScriptableGenerator.bindings.cs:414>
		RuntimeObject* L_2 = ___0_obj;
		if (!((RuntimeObject*)IsInstSealed((RuntimeObject*)L_2, GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2_il2cpp_TypeInfo_var)))
		{
			goto IL_0025;
		}
	}
	{
		RuntimeObject* L_3 = ___0_obj;
		V_0 = ((*(GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2*)UnBox(L_3, GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2_il2cpp_TypeInfo_var)));
		GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2 L_4 = V_0;
		bool L_5;
		L_5 = GeneratorInstance_Equals_m1FEFD7862F181D69396710522A061AA1A5189FE1(__this, L_4, NULL);
		G_B5_0 = ((int32_t)(L_5));
		goto IL_0026;
	}

IL_0025:
	{
		G_B5_0 = 0;
	}

IL_0026:
	{
		V_2 = (bool)G_B5_0;
		goto IL_0029;
	}

IL_0029:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ScriptableGenerator.bindings.cs:415>
		bool L_6 = V_2;
		return L_6;
	}
}
IL2CPP_EXTERN_C  bool GeneratorInstance_Equals_m7412F772B53B5052FF5070FB2BB2099A93270206_AdjustorThunk (RuntimeObject* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method)
{
	GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2*>(__this + _offset);
	bool _returnValue;
	_returnValue = GeneratorInstance_Equals_m7412F772B53B5052FF5070FB2BB2099A93270206(_thisAdjusted, ___0_obj, method);
	return _returnValue;
}
// Method Definition Index: 87539
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t GeneratorInstance_GetHashCode_mBF9BB29BE3D212AAAEF53A02346FED4B0435C7D2 (GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2* __this, const RuntimeMethod* method) 
{
	int32_t V_0 = 0;
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ScriptableGenerator.bindings.cs:445>
		ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35* L_0 = (ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35*)(&__this->___m_ProcessorInstance);
		int32_t L_1;
		L_1 = ProcessorInstance_GetHashCode_m607D39F5BA947B94238348CF61431A9D8049F9B0(L_0, NULL);
		V_0 = L_1;
		goto IL_0015;
	}

IL_0015:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ScriptableGenerator.bindings.cs:446>
		int32_t L_2 = V_0;
		return L_2;
	}
}
IL2CPP_EXTERN_C  int32_t GeneratorInstance_GetHashCode_mBF9BB29BE3D212AAAEF53A02346FED4B0435C7D2_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2*>(__this + _offset);
	int32_t _returnValue;
	_returnValue = GeneratorInstance_GetHashCode_mBF9BB29BE3D212AAAEF53A02346FED4B0435C7D2(_thisAdjusted, method);
	return _returnValue;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C void Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshal_pinvoke(const Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060& unmarshaled, Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_pinvoke& marshaled)
{
	marshaled.___Setup = unmarshaled.___Setup;
	marshaled.___Properties = unmarshaled.___Properties;
	marshaled.___ReportedLength = unmarshaled.___ReportedLength;
	marshaled.___IsFinite = static_cast<int32_t>(unmarshaled.___IsFinite);
	marshaled.___IsRealtime = static_cast<int32_t>(unmarshaled.___IsRealtime);
	marshaled.___HasKnownLength = static_cast<int32_t>(unmarshaled.___HasKnownLength);
}
IL2CPP_EXTERN_C void Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshal_pinvoke_back(const Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_pinvoke& marshaled, Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060& unmarshaled)
{
	Setup_t24BE50E09A2A2C409ED7EC3BCEB0B8ACD458A503 unmarshaledSetup_temp_0;
	memset((&unmarshaledSetup_temp_0), 0, sizeof(unmarshaledSetup_temp_0));
	unmarshaledSetup_temp_0 = marshaled.___Setup;
	unmarshaled.___Setup = unmarshaledSetup_temp_0;
	Properties_tA0A739E222CC4DF46C8E1F08B908950539845092 unmarshaledProperties_temp_1;
	memset((&unmarshaledProperties_temp_1), 0, sizeof(unmarshaledProperties_temp_1));
	unmarshaledProperties_temp_1 = marshaled.___Properties;
	unmarshaled.___Properties = unmarshaledProperties_temp_1;
	DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E unmarshaledReportedLength_temp_2;
	memset((&unmarshaledReportedLength_temp_2), 0, sizeof(unmarshaledReportedLength_temp_2));
	unmarshaledReportedLength_temp_2 = marshaled.___ReportedLength;
	unmarshaled.___ReportedLength = unmarshaledReportedLength_temp_2;
	bool unmarshaledIsFinite_temp_3 = false;
	unmarshaledIsFinite_temp_3 = static_cast<bool>(marshaled.___IsFinite);
	unmarshaled.___IsFinite = unmarshaledIsFinite_temp_3;
	bool unmarshaledIsRealtime_temp_4 = false;
	unmarshaledIsRealtime_temp_4 = static_cast<bool>(marshaled.___IsRealtime);
	unmarshaled.___IsRealtime = unmarshaledIsRealtime_temp_4;
	bool unmarshaledHasKnownLength_temp_5 = false;
	unmarshaledHasKnownLength_temp_5 = static_cast<bool>(marshaled.___HasKnownLength);
	unmarshaled.___HasKnownLength = unmarshaledHasKnownLength_temp_5;
}
IL2CPP_EXTERN_C void Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshal_pinvoke_cleanup(Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_pinvoke& marshaled)
{
}
IL2CPP_EXTERN_C void Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshal_com(const Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060& unmarshaled, Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_com& marshaled)
{
	marshaled.___Setup = unmarshaled.___Setup;
	marshaled.___Properties = unmarshaled.___Properties;
	marshaled.___ReportedLength = unmarshaled.___ReportedLength;
	marshaled.___IsFinite = static_cast<int32_t>(unmarshaled.___IsFinite);
	marshaled.___IsRealtime = static_cast<int32_t>(unmarshaled.___IsRealtime);
	marshaled.___HasKnownLength = static_cast<int32_t>(unmarshaled.___HasKnownLength);
}
IL2CPP_EXTERN_C void Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshal_com_back(const Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_com& marshaled, Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060& unmarshaled)
{
	Setup_t24BE50E09A2A2C409ED7EC3BCEB0B8ACD458A503 unmarshaledSetup_temp_0;
	memset((&unmarshaledSetup_temp_0), 0, sizeof(unmarshaledSetup_temp_0));
	unmarshaledSetup_temp_0 = marshaled.___Setup;
	unmarshaled.___Setup = unmarshaledSetup_temp_0;
	Properties_tA0A739E222CC4DF46C8E1F08B908950539845092 unmarshaledProperties_temp_1;
	memset((&unmarshaledProperties_temp_1), 0, sizeof(unmarshaledProperties_temp_1));
	unmarshaledProperties_temp_1 = marshaled.___Properties;
	unmarshaled.___Properties = unmarshaledProperties_temp_1;
	DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E unmarshaledReportedLength_temp_2;
	memset((&unmarshaledReportedLength_temp_2), 0, sizeof(unmarshaledReportedLength_temp_2));
	unmarshaledReportedLength_temp_2 = marshaled.___ReportedLength;
	unmarshaled.___ReportedLength = unmarshaledReportedLength_temp_2;
	bool unmarshaledIsFinite_temp_3 = false;
	unmarshaledIsFinite_temp_3 = static_cast<bool>(marshaled.___IsFinite);
	unmarshaled.___IsFinite = unmarshaledIsFinite_temp_3;
	bool unmarshaledIsRealtime_temp_4 = false;
	unmarshaledIsRealtime_temp_4 = static_cast<bool>(marshaled.___IsRealtime);
	unmarshaled.___IsRealtime = unmarshaledIsRealtime_temp_4;
	bool unmarshaledHasKnownLength_temp_5 = false;
	unmarshaledHasKnownLength_temp_5 = static_cast<bool>(marshaled.___HasKnownLength);
	unmarshaled.___HasKnownLength = unmarshaledHasKnownLength_temp_5;
}
IL2CPP_EXTERN_C void Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshal_com_cleanup(Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshaled_com& marshaled)
{
}
// Method Definition Index: 87543
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Configuration_get_isRealtime_m99E988C200947A8277C144A84CB1755B5A0C2D78 (Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060* __this, const RuntimeMethod* method) 
{
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ScriptableGenerator.bindings.cs:255>
		bool L_0 = __this->___IsRealtime;
		return L_0;
	}
}
IL2CPP_EXTERN_C  bool Configuration_get_isRealtime_m99E988C200947A8277C144A84CB1755B5A0C2D78_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060*>(__this + _offset);
	bool _returnValue;
	_returnValue = Configuration_get_isRealtime_m99E988C200947A8277C144A84CB1755B5A0C2D78_inline(_thisAdjusted, method);
	return _returnValue;
}
// Method Definition Index: 87544
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 Configuration_get_length_mB699EC1CF6B8A0955A60B55ABB22AD00A3F6BC3B (Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Nullable_1__ctor_mA3535F58A54E060F67F4FFB3A5974A10D6444604_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 G_B3_0;
	memset((&G_B3_0), 0, sizeof(G_B3_0));
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ScriptableGenerator.bindings.cs:260>
		bool L_0 = __this->___HasKnownLength;
		if (L_0)
		{
			goto IL_0013;
		}
	}
	{
		il2cpp_codegen_initobj((&V_0), sizeof(Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363));
		Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 L_1 = V_0;
		G_B3_0 = L_1;
		goto IL_001e;
	}

IL_0013:
	{
		DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E L_2 = __this->___ReportedLength;
		Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 L_3;
		memset((&L_3), 0, sizeof(L_3));
		Nullable_1__ctor_mA3535F58A54E060F67F4FFB3A5974A10D6444604((&L_3), L_2, Nullable_1__ctor_mA3535F58A54E060F67F4FFB3A5974A10D6444604_RuntimeMethod_var);
		G_B3_0 = L_3;
	}

IL_001e:
	{
		return G_B3_0;
	}
}
IL2CPP_EXTERN_C  Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 Configuration_get_length_mB699EC1CF6B8A0955A60B55ABB22AD00A3F6BC3B_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060*>(__this + _offset);
	Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 _returnValue;
	_returnValue = Configuration_get_length_mB699EC1CF6B8A0955A60B55ABB22AD00A3F6BC3B(_thisAdjusted, method);
	return _returnValue;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif


IL2CPP_EXTERN_C void GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0_marshal_pinvoke(const GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0& unmarshaled, GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0_marshaled_pinvoke& marshaled)
{
	marshaled.___Processor = unmarshaled.___Processor;
	Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshal_pinvoke(unmarshaled.___Configuration, marshaled.___Configuration);
}
IL2CPP_EXTERN_C void GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0_marshal_pinvoke_back(const GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0_marshaled_pinvoke& marshaled, GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0& unmarshaled)
{
	ProcessorHeader_t2A43F64ECCCC872761CDBE0DA7A36DB193CA1085 unmarshaledProcessor_temp_0;
	memset((&unmarshaledProcessor_temp_0), 0, sizeof(unmarshaledProcessor_temp_0));
	unmarshaledProcessor_temp_0 = marshaled.___Processor;
	unmarshaled.___Processor = unmarshaledProcessor_temp_0;
	Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060 unmarshaledConfiguration_temp_1;
	memset((&unmarshaledConfiguration_temp_1), 0, sizeof(unmarshaledConfiguration_temp_1));
	Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshal_pinvoke_back(marshaled.___Configuration, unmarshaledConfiguration_temp_1);
	unmarshaled.___Configuration = unmarshaledConfiguration_temp_1;
}
IL2CPP_EXTERN_C void GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0_marshal_pinvoke_cleanup(GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0_marshaled_pinvoke& marshaled)
{
	Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshal_pinvoke_cleanup(marshaled.___Configuration);
}


IL2CPP_EXTERN_C void GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0_marshal_com(const GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0& unmarshaled, GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0_marshaled_com& marshaled)
{
	marshaled.___Processor = unmarshaled.___Processor;
	Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshal_com(unmarshaled.___Configuration, marshaled.___Configuration);
}
IL2CPP_EXTERN_C void GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0_marshal_com_back(const GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0_marshaled_com& marshaled, GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0& unmarshaled)
{
	ProcessorHeader_t2A43F64ECCCC872761CDBE0DA7A36DB193CA1085 unmarshaledProcessor_temp_0;
	memset((&unmarshaledProcessor_temp_0), 0, sizeof(unmarshaledProcessor_temp_0));
	unmarshaledProcessor_temp_0 = marshaled.___Processor;
	unmarshaled.___Processor = unmarshaledProcessor_temp_0;
	Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060 unmarshaledConfiguration_temp_1;
	memset((&unmarshaledConfiguration_temp_1), 0, sizeof(unmarshaledConfiguration_temp_1));
	Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshal_com_back(marshaled.___Configuration, unmarshaledConfiguration_temp_1);
	unmarshaled.___Configuration = unmarshaledConfiguration_temp_1;
}
IL2CPP_EXTERN_C void GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0_marshal_com_cleanup(GeneratorHeader_t73B89668B0DA457A4F3141DE6AC8B72D5ED0BDF0_marshaled_com& marshaled)
{
	Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060_marshal_com_cleanup(marshaled.___Configuration);
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 87545
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ProcessorInstance_Equals_m4873E7D141484FCADC7F0F9C64542D57E0D02398 (ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35* __this, ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 ___0_other, const RuntimeMethod* method) 
{
	bool V_0 = false;
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ScriptableProcessor.bindings.cs:461>
		Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* L_0 = (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153*)(&__this->___Handle);
		ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 L_1 = ___0_other;
		Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153 L_2 = L_1.___Handle;
		bool L_3;
		L_3 = Handle_Equals_m5D8356E5FCA91FFFEACC910C738C37690EBA2E64(L_0, L_2, NULL);
		V_0 = L_3;
		goto IL_0015;
	}

IL_0015:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ScriptableProcessor.bindings.cs:462>
		bool L_4 = V_0;
		return L_4;
	}
}
IL2CPP_EXTERN_C  bool ProcessorInstance_Equals_m4873E7D141484FCADC7F0F9C64542D57E0D02398_AdjustorThunk (RuntimeObject* __this, ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 ___0_other, const RuntimeMethod* method)
{
	ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35*>(__this + _offset);
	bool _returnValue;
	_returnValue = ProcessorInstance_Equals_m4873E7D141484FCADC7F0F9C64542D57E0D02398(_thisAdjusted, ___0_other, method);
	return _returnValue;
}
// Method Definition Index: 87546
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ProcessorInstance_Equals_mFE0F0DA5B311AD6D34EABD73F61C4AE3014084ED (ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 V_0;
	memset((&V_0), 0, sizeof(V_0));
	bool V_1 = false;
	bool V_2 = false;
	int32_t G_B5_0 = 0;
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ScriptableProcessor.bindings.cs:471>
		RuntimeObject* L_0 = ___0_obj;
		V_1 = (bool)((((RuntimeObject*)(RuntimeObject*)L_0) == ((RuntimeObject*)(RuntimeObject*)NULL))? 1 : 0);
		bool L_1 = V_1;
		if (!L_1)
		{
			goto IL_000d;
		}
	}
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ScriptableProcessor.bindings.cs:472>
		V_2 = (bool)0;
		goto IL_0029;
	}

IL_000d:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ScriptableProcessor.bindings.cs:474>
		RuntimeObject* L_2 = ___0_obj;
		if (!((RuntimeObject*)IsInstSealed((RuntimeObject*)L_2, ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35_il2cpp_TypeInfo_var)))
		{
			goto IL_0025;
		}
	}
	{
		RuntimeObject* L_3 = ___0_obj;
		V_0 = ((*(ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35*)UnBox(L_3, ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35_il2cpp_TypeInfo_var)));
		ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 L_4 = V_0;
		bool L_5;
		L_5 = ProcessorInstance_Equals_m4873E7D141484FCADC7F0F9C64542D57E0D02398(__this, L_4, NULL);
		G_B5_0 = ((int32_t)(L_5));
		goto IL_0026;
	}

IL_0025:
	{
		G_B5_0 = 0;
	}

IL_0026:
	{
		V_2 = (bool)G_B5_0;
		goto IL_0029;
	}

IL_0029:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ScriptableProcessor.bindings.cs:475>
		bool L_6 = V_2;
		return L_6;
	}
}
IL2CPP_EXTERN_C  bool ProcessorInstance_Equals_mFE0F0DA5B311AD6D34EABD73F61C4AE3014084ED_AdjustorThunk (RuntimeObject* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method)
{
	ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35*>(__this + _offset);
	bool _returnValue;
	_returnValue = ProcessorInstance_Equals_mFE0F0DA5B311AD6D34EABD73F61C4AE3014084ED(_thisAdjusted, ___0_obj, method);
	return _returnValue;
}
// Method Definition Index: 87547
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ProcessorInstance_GetHashCode_m607D39F5BA947B94238348CF61431A9D8049F9B0 (ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35* __this, const RuntimeMethod* method) 
{
	int32_t V_0 = 0;
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ScriptableProcessor.bindings.cs:505>
		Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* L_0 = (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153*)(&__this->___Handle);
		int32_t L_1;
		L_1 = Handle_GetHashCode_m8DD5E2EE5197C1FF37CD2CF3B4FD892A9E299DFE(L_0, NULL);
		V_0 = L_1;
		goto IL_0015;
	}

IL_0015:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ScriptableProcessor.bindings.cs:506>
		int32_t L_2 = V_0;
		return L_2;
	}
}
IL2CPP_EXTERN_C  int32_t ProcessorInstance_GetHashCode_m607D39F5BA947B94238348CF61431A9D8049F9B0_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35*>(__this + _offset);
	int32_t _returnValue;
	_returnValue = ProcessorInstance_GetHashCode_m607D39F5BA947B94238348CF61431A9D8049F9B0(_thisAdjusted, method);
	return _returnValue;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 87549
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ScriptableGeneratorBindings_InstantiateGeneratorFromObject_m627DB7836271F2551E83B21A49E39952BA58D7B3 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___0_generatorObjectDefinition, ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67* ___1_control, GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2* ___2_runtimeHandle, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IAudioGenerator_tD9DD08E5891ADC8BBD55CA6E1F00C0EED5C816DA_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ICapabilities_tF49934ED8BF0930F82A0A54F794949FB61194793_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Nullable_1_GetValueOrDefault_m5879DF28E8CBC03BD7B2F3A7B383BB0068DE64DC_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Nullable_1_get_HasValue_m2C09C399366A16930BBC996441FE7CED2907D3D0_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral0D4DFFEDA70FA6D865B68C20C1F40F10FC31B6DD);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral5A10D0C56FEA8FB8816E3E1F6F88D55578F844F7);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral5C2C12AF4EFFBF824B747CFFBFC316CAE24077D1);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralC767BA736F2BEEB0EC1BA27ABE77214EC763C9B6);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralEE560D196E71D95F90A39910549B06428576F27C);
		s_Il2CppMethodInitialized = true;
	}
	RuntimeObject* V_0 = NULL;
	bool V_1 = false;
	ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67* V_2 = NULL;
	ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67* V_3 = NULL;
	ControlContext_t67F00D3C59C26C77BED313684AB258AEC5C5A243 V_4;
	memset((&V_4), 0, sizeof(V_4));
	Nullable_1_t8743998EB2E7DB936C93772849EF306FBD5C5EE5 V_5;
	memset((&V_5), 0, sizeof(V_5));
	CreationParameters_tC3AF64A7D0E7DAF87A587B34097DF91B377A9BD7 V_6;
	memset((&V_6), 0, sizeof(V_6));
	bool V_7 = false;
	Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060 V_8;
	memset((&V_8), 0, sizeof(V_8));
	bool V_9 = false;
	bool V_10 = false;
	bool V_11 = false;
	Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 V_12;
	memset((&V_12), 0, sizeof(V_12));
	Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 V_13;
	memset((&V_13), 0, sizeof(V_13));
	int32_t G_B11_0 = 0;
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ScriptableGenerator.bindings.cs:614>
		Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* L_0 = ___0_generatorObjectDefinition;
		V_0 = ((RuntimeObject*)IsInst((RuntimeObject*)L_0, IAudioGenerator_tD9DD08E5891ADC8BBD55CA6E1F00C0EED5C816DA_il2cpp_TypeInfo_var));
		RuntimeObject* L_1 = V_0;
		V_1 = (bool)((!(((RuntimeObject*)(RuntimeObject*)L_1) <= ((RuntimeObject*)(RuntimeObject*)NULL)))? 1 : 0);
		bool L_2 = V_1;
		if (!L_2)
		{
			goto IL_0169;
		}
	}
	{
		ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67* L_3 = ___1_control;
		V_3 = L_3;
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ScriptableGenerator.bindings.cs:616>
		ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67* L_4 = V_3;
		V_2 = (ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67*)((uintptr_t)L_4);
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ScriptableGenerator.bindings.cs:618>
		ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67* L_5 = V_2;
		ControlContext__ctor_mFFC3A14E72F61DACB51F4F771FAE8416702023B6((&V_4), (void*)L_5, NULL);
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ScriptableGenerator.bindings.cs:619>
		GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2* L_6 = ___2_runtimeHandle;
		RuntimeObject* L_7 = V_0;
		ControlContext_t67F00D3C59C26C77BED313684AB258AEC5C5A243 L_8 = V_4;
		il2cpp_codegen_initobj((&V_5), sizeof(Nullable_1_t8743998EB2E7DB936C93772849EF306FBD5C5EE5));
		Nullable_1_t8743998EB2E7DB936C93772849EF306FBD5C5EE5 L_9 = V_5;
		il2cpp_codegen_initobj((&V_6), sizeof(CreationParameters_tC3AF64A7D0E7DAF87A587B34097DF91B377A9BD7));
		CreationParameters_tC3AF64A7D0E7DAF87A587B34097DF91B377A9BD7 L_10 = V_6;
		NullCheck(L_7);
		GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2 L_11;
		L_11 = InterfaceFuncInvoker3< GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2, ControlContext_t67F00D3C59C26C77BED313684AB258AEC5C5A243, Nullable_1_t8743998EB2E7DB936C93772849EF306FBD5C5EE5, CreationParameters_tC3AF64A7D0E7DAF87A587B34097DF91B377A9BD7 >::Invoke(0, IAudioGenerator_tD9DD08E5891ADC8BBD55CA6E1F00C0EED5C816DA_il2cpp_TypeInfo_var, L_7, L_8, L_9, L_10);
		*(GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2*)L_6 = L_11;
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ScriptableGenerator.bindings.cs:621>
		GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2* L_12 = ___2_runtimeHandle;
		ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 L_13;
		L_13 = GeneratorInstance_op_Implicit_mB7A6E46130D3BAB42E00F4DD08155EF286A16167_inline(L_12, NULL);
		bool L_14;
		L_14 = ControlContext_Exists_mEEB0CCB244CABE9EAD81EBC5E54CB6B50E18FA0F((&V_4), L_13, NULL);
		V_7 = L_14;
		bool L_15 = V_7;
		if (!L_15)
		{
			goto IL_0162;
		}
	}
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ScriptableGenerator.bindings.cs:623>
		GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2* L_16 = ___2_runtimeHandle;
		GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2 L_17 = (*(GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2*)L_16);
		Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060 L_18;
		L_18 = ControlContext_GetConfiguration_m5EF5175CD07C7E1661F6AC36ECEA52A293DB0249((&V_4), L_17, NULL);
		V_8 = L_18;
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ScriptableGenerator.bindings.cs:625>
		RuntimeObject* L_19 = V_0;
		NullCheck(L_19);
		bool L_20;
		L_20 = InterfaceFuncInvoker0< bool >::Invoke(0, ICapabilities_tF49934ED8BF0930F82A0A54F794949FB61194793_il2cpp_TypeInfo_var, L_19);
		Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060 L_21 = V_8;
		bool L_22 = L_21.___IsFinite;
		V_9 = (bool)((((int32_t)((((int32_t)L_20) == ((int32_t)L_22))? 1 : 0)) == ((int32_t)0))? 1 : 0);
		bool L_23 = V_9;
		if (!L_23)
		{
			goto IL_00ac;
		}
	}
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ScriptableGenerator.bindings.cs:627>
		Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* L_24 = ___0_generatorObjectDefinition;
		RuntimeObject* L_25 = V_0;
		NullCheck(L_25);
		bool L_26;
		L_26 = InterfaceFuncInvoker0< bool >::Invoke(0, ICapabilities_tF49934ED8BF0930F82A0A54F794949FB61194793_il2cpp_TypeInfo_var, L_25);
		bool L_27 = L_26;
		RuntimeObject* L_28 = Box(il2cpp_defaults.boolean_class, &L_27);
		Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060 L_29 = V_8;
		bool L_30 = L_29.___IsFinite;
		bool L_31 = L_30;
		RuntimeObject* L_32 = Box(il2cpp_defaults.boolean_class, &L_31);
		String_t* L_33;
		L_33 = String_Format_mA0534D6E2AE4D67A6BD8D45B3321323930EB930C(_stringLiteral5C2C12AF4EFFBF824B747CFFBFC316CAE24077D1, L_24, L_28, L_32, NULL);
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_LogError_mB00B2B4468EF3CAF041B038D840820FB84C924B2(L_33, NULL);
	}

IL_00ac:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ScriptableGenerator.bindings.cs:630>
		RuntimeObject* L_34 = V_0;
		NullCheck(L_34);
		bool L_35;
		L_35 = InterfaceFuncInvoker0< bool >::Invoke(1, ICapabilities_tF49934ED8BF0930F82A0A54F794949FB61194793_il2cpp_TypeInfo_var, L_34);
		bool L_36;
		L_36 = Configuration_get_isRealtime_m99E988C200947A8277C144A84CB1755B5A0C2D78_inline((&V_8), NULL);
		V_10 = (bool)((((int32_t)((((int32_t)L_35) == ((int32_t)L_36))? 1 : 0)) == ((int32_t)0))? 1 : 0);
		bool L_37 = V_10;
		if (!L_37)
		{
			goto IL_00ee;
		}
	}
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ScriptableGenerator.bindings.cs:632>
		Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* L_38 = ___0_generatorObjectDefinition;
		RuntimeObject* L_39 = V_0;
		NullCheck(L_39);
		bool L_40;
		L_40 = InterfaceFuncInvoker0< bool >::Invoke(1, ICapabilities_tF49934ED8BF0930F82A0A54F794949FB61194793_il2cpp_TypeInfo_var, L_39);
		bool L_41 = L_40;
		RuntimeObject* L_42 = Box(il2cpp_defaults.boolean_class, &L_41);
		bool L_43;
		L_43 = Configuration_get_isRealtime_m99E988C200947A8277C144A84CB1755B5A0C2D78_inline((&V_8), NULL);
		bool L_44 = L_43;
		RuntimeObject* L_45 = Box(il2cpp_defaults.boolean_class, &L_44);
		String_t* L_46;
		L_46 = String_Format_mA0534D6E2AE4D67A6BD8D45B3321323930EB930C(_stringLiteral0D4DFFEDA70FA6D865B68C20C1F40F10FC31B6DD, L_38, L_42, L_45, NULL);
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_LogError_mB00B2B4468EF3CAF041B038D840820FB84C924B2(L_46, NULL);
	}

IL_00ee:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ScriptableGenerator.bindings.cs:635>
		RuntimeObject* L_47 = V_0;
		NullCheck(L_47);
		Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 L_48;
		L_48 = InterfaceFuncInvoker0< Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 >::Invoke(2, ICapabilities_tF49934ED8BF0930F82A0A54F794949FB61194793_il2cpp_TypeInfo_var, L_47);
		V_12 = L_48;
		Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 L_49;
		L_49 = Configuration_get_length_mB699EC1CF6B8A0955A60B55ABB22AD00A3F6BC3B((&V_8), NULL);
		V_13 = L_49;
		bool L_50;
		L_50 = Nullable_1_get_HasValue_m2C09C399366A16930BBC996441FE7CED2907D3D0_inline((&V_12), Nullable_1_get_HasValue_m2C09C399366A16930BBC996441FE7CED2907D3D0_RuntimeMethod_var);
		bool L_51;
		L_51 = Nullable_1_get_HasValue_m2C09C399366A16930BBC996441FE7CED2907D3D0_inline((&V_13), Nullable_1_get_HasValue_m2C09C399366A16930BBC996441FE7CED2907D3D0_RuntimeMethod_var);
		if ((((int32_t)L_50) == ((int32_t)L_51)))
		{
			goto IL_0112;
		}
	}
	{
		G_B11_0 = 1;
		goto IL_0131;
	}

IL_0112:
	{
		bool L_52;
		L_52 = Nullable_1_get_HasValue_m2C09C399366A16930BBC996441FE7CED2907D3D0_inline((&V_12), Nullable_1_get_HasValue_m2C09C399366A16930BBC996441FE7CED2907D3D0_RuntimeMethod_var);
		if (L_52)
		{
			goto IL_011e;
		}
	}
	{
		G_B11_0 = 0;
		goto IL_0131;
	}

IL_011e:
	{
		DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E L_53;
		L_53 = Nullable_1_GetValueOrDefault_m5879DF28E8CBC03BD7B2F3A7B383BB0068DE64DC_inline((&V_12), Nullable_1_GetValueOrDefault_m5879DF28E8CBC03BD7B2F3A7B383BB0068DE64DC_RuntimeMethod_var);
		DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E L_54;
		L_54 = Nullable_1_GetValueOrDefault_m5879DF28E8CBC03BD7B2F3A7B383BB0068DE64DC_inline((&V_13), Nullable_1_GetValueOrDefault_m5879DF28E8CBC03BD7B2F3A7B383BB0068DE64DC_RuntimeMethod_var);
		il2cpp_codegen_runtime_class_init_inline(DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E_il2cpp_TypeInfo_var);
		bool L_55;
		L_55 = DiscreteTime_op_Inequality_m4C12F6A54E5C6E8BAC45A3903E6C2A3127C6063C_inline(L_53, L_54, NULL);
		G_B11_0 = ((int32_t)(L_55));
	}

IL_0131:
	{
		V_11 = (bool)G_B11_0;
		bool L_56 = V_11;
		if (!L_56)
		{
			goto IL_0161;
		}
	}
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ScriptableGenerator.bindings.cs:637>
		Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* L_57 = ___0_generatorObjectDefinition;
		RuntimeObject* L_58 = V_0;
		NullCheck(L_58);
		Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 L_59;
		L_59 = InterfaceFuncInvoker0< Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 >::Invoke(2, ICapabilities_tF49934ED8BF0930F82A0A54F794949FB61194793_il2cpp_TypeInfo_var, L_58);
		Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 L_60 = L_59;
		RuntimeObject* L_61 = Box(Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363_il2cpp_TypeInfo_var, &L_60);
		Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 L_62;
		L_62 = Configuration_get_length_mB699EC1CF6B8A0955A60B55ABB22AD00A3F6BC3B((&V_8), NULL);
		Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363 L_63 = L_62;
		RuntimeObject* L_64 = Box(Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363_il2cpp_TypeInfo_var, &L_63);
		String_t* L_65;
		L_65 = String_Format_mA0534D6E2AE4D67A6BD8D45B3321323930EB930C(_stringLiteralEE560D196E71D95F90A39910549B06428576F27C, L_57, L_61, L_64, NULL);
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_LogError_mB00B2B4468EF3CAF041B038D840820FB84C924B2(L_65, NULL);
	}

IL_0161:
	{
	}

IL_0162:
	{
		V_3 = (ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67*)((uintptr_t)0);
		goto IL_0188;
	}

IL_0169:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ScriptableGenerator.bindings.cs:644>
		GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2* L_66 = ___2_runtimeHandle;
		il2cpp_codegen_initobj(L_66, sizeof(GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2));
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ScriptableGenerator.bindings.cs:645>
		Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* L_67 = ___0_generatorObjectDefinition;
		String_t* L_68;
		L_68 = String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987(_stringLiteralC767BA736F2BEEB0EC1BA27ABE77214EC763C9B6, L_67, _stringLiteral5A10D0C56FEA8FB8816E3E1F6F88D55578F844F7, NULL);
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_LogError_mB00B2B4468EF3CAF041B038D840820FB84C924B2(L_68, NULL);
	}

IL_0188:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ScriptableGenerator.bindings.cs:647>
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 87550
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ScriptableProcessorBindings_CheckProcessorExists_mCA17BD1A2B9E776034C7B312CA810B3B739AA04E (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153 ___0_handle, ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67* ___1_control, const RuntimeMethod* method) 
{
	bool V_0 = false;
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ScriptableProcessor.bindings.cs:680>
		Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153 L_0 = ___0_handle;
		ControlHeader_t88FD879CA9C5610D6410BA56087244CEA5DE5A67* L_1 = ___1_control;
		bool L_2;
		L_2 = ScriptableProcessorBindings_CheckProcessorExistsInternal_mF6C33BA9D06B46EAAA47D05E74BA20CC604141A6(L_0, (void*)L_1, NULL);
		V_0 = L_2;
		goto IL_000b;
	}

IL_000b:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ScriptableProcessor.bindings.cs:681>
		bool L_3 = V_0;
		return L_3;
	}
}
// Method Definition Index: 87551
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ScriptableProcessorBindings_CheckProcessorExistsInternal_mF6C33BA9D06B46EAAA47D05E74BA20CC604141A6 (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153 ___0_handle, void* ___1_control, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		void* L_0 = ___1_control;
		bool L_1;
		L_1 = ScriptableProcessorBindings_CheckProcessorExistsInternal_Injected_mD29018C86FF29463FEC0346E4D8308F7F6EA1831((&___0_handle), L_0, NULL);
		return L_1;
	}
}
// Method Definition Index: 87552
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ScriptableProcessorBindings_CheckProcessorExistsInternal_Injected_mD29018C86FF29463FEC0346E4D8308F7F6EA1831 (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* ___0_handle, void* ___1_control, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	typedef bool (*ScriptableProcessorBindings_CheckProcessorExistsInternal_Injected_mD29018C86FF29463FEC0346E4D8308F7F6EA1831_ftn) (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153*, void*);
	static ScriptableProcessorBindings_CheckProcessorExistsInternal_Injected_mD29018C86FF29463FEC0346E4D8308F7F6EA1831_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (ScriptableProcessorBindings_CheckProcessorExistsInternal_Injected_mD29018C86FF29463FEC0346E4D8308F7F6EA1831_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.Audio.ScriptableProcessorBindings::CheckProcessorExistsInternal_Injected(Unity.Audio.Handle&,System.Void*)");
	bool icallRetVal = _il2cpp_icall_func(___0_handle, ___1_control);
	return icallRetVal;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 87553
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Node_tE9DD997AC2A1CDC0DF9579D3B88467E7F42142C8* Handle_get_AtomicNode_m3D7AF7852BDF5999877F8F973FDF8724974B0A50 (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* __this, const RuntimeMethod* method) 
{
	Node_tE9DD997AC2A1CDC0DF9579D3B88467E7F42142C8* V_0 = NULL;
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/AudioHandle.bindings.cs:19>
		intptr_t L_0 = __this->___m_Node;
		void* L_1;
		L_1 = IntPtr_op_Explicit_m2728CBA081E79B97DDCF1D4FAD77B309CA1E94BF(L_0, NULL);
		V_0 = (Node_tE9DD997AC2A1CDC0DF9579D3B88467E7F42142C8*)L_1;
		goto IL_000f;
	}

IL_000f:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/AudioHandle.bindings.cs:19>
		Node_tE9DD997AC2A1CDC0DF9579D3B88467E7F42142C8* L_2 = V_0;
		return L_2;
	}
}
IL2CPP_EXTERN_C  Node_tE9DD997AC2A1CDC0DF9579D3B88467E7F42142C8* Handle_get_AtomicNode_m3D7AF7852BDF5999877F8F973FDF8724974B0A50_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153*>(__this + _offset);
	Node_tE9DD997AC2A1CDC0DF9579D3B88467E7F42142C8* _returnValue;
	_returnValue = Handle_get_AtomicNode_m3D7AF7852BDF5999877F8F973FDF8724974B0A50(_thisAdjusted, method);
	return _returnValue;
}
// Method Definition Index: 87554
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Handle_Equals_m5D8356E5FCA91FFFEACC910C738C37690EBA2E64 (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* __this, Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153 ___0_other, const RuntimeMethod* method) 
{
	bool V_0 = false;
	int32_t G_B3_0 = 0;
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/AudioHandle.bindings.cs:66>
		intptr_t L_0 = __this->___m_Node;
		Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153 L_1 = ___0_other;
		intptr_t L_2 = L_1.___m_Node;
		bool L_3;
		L_3 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_0, L_2, NULL);
		if (!L_3)
		{
			goto IL_0024;
		}
	}
	{
		int32_t L_4 = __this->___Version;
		Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153 L_5 = ___0_other;
		int32_t L_6 = L_5.___Version;
		G_B3_0 = ((((int32_t)L_4) == ((int32_t)L_6))? 1 : 0);
		goto IL_0025;
	}

IL_0024:
	{
		G_B3_0 = 0;
	}

IL_0025:
	{
		V_0 = (bool)G_B3_0;
		goto IL_0028;
	}

IL_0028:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/AudioHandle.bindings.cs:67>
		bool L_7 = V_0;
		return L_7;
	}
}
IL2CPP_EXTERN_C  bool Handle_Equals_m5D8356E5FCA91FFFEACC910C738C37690EBA2E64_AdjustorThunk (RuntimeObject* __this, Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153 ___0_other, const RuntimeMethod* method)
{
	Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153*>(__this + _offset);
	bool _returnValue;
	_returnValue = Handle_Equals_m5D8356E5FCA91FFFEACC910C738C37690EBA2E64(_thisAdjusted, ___0_other, method);
	return _returnValue;
}
// Method Definition Index: 87555
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Handle_Equals_mDE217E745B1016A74BCE82848675ED913B391B8F (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	bool V_1 = false;
	int32_t G_B5_0 = 0;
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/AudioHandle.bindings.cs:71>
		RuntimeObject* L_0 = ___0_obj;
		V_0 = (bool)((((RuntimeObject*)(RuntimeObject*)L_0) == ((RuntimeObject*)(RuntimeObject*)NULL))? 1 : 0);
		bool L_1 = V_0;
		if (!L_1)
		{
			goto IL_000d;
		}
	}
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/AudioHandle.bindings.cs:71>
		V_1 = (bool)0;
		goto IL_0027;
	}

IL_000d:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/AudioHandle.bindings.cs:72>
		RuntimeObject* L_2 = ___0_obj;
		if (!((RuntimeObject*)IsInstSealed((RuntimeObject*)L_2, Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153_il2cpp_TypeInfo_var)))
		{
			goto IL_0023;
		}
	}
	{
		RuntimeObject* L_3 = ___0_obj;
		bool L_4;
		L_4 = Handle_Equals_m5D8356E5FCA91FFFEACC910C738C37690EBA2E64(__this, ((*(Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153*)UnBox(L_3, Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153_il2cpp_TypeInfo_var))), NULL);
		G_B5_0 = ((int32_t)(L_4));
		goto IL_0024;
	}

IL_0023:
	{
		G_B5_0 = 0;
	}

IL_0024:
	{
		V_1 = (bool)G_B5_0;
		goto IL_0027;
	}

IL_0027:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/AudioHandle.bindings.cs:73>
		bool L_5 = V_1;
		return L_5;
	}
}
IL2CPP_EXTERN_C  bool Handle_Equals_mDE217E745B1016A74BCE82848675ED913B391B8F_AdjustorThunk (RuntimeObject* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method)
{
	Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153*>(__this + _offset);
	bool _returnValue;
	_returnValue = Handle_Equals_mDE217E745B1016A74BCE82848675ED913B391B8F(_thisAdjusted, ___0_obj, method);
	return _returnValue;
}
// Method Definition Index: 87556
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Handle_GetHashCode_m8DD5E2EE5197C1FF37CD2CF3B4FD892A9E299DFE (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* __this, const RuntimeMethod* method) 
{
	int32_t V_0 = 0;
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/AudioHandle.bindings.cs:79>
		intptr_t L_0 = __this->___m_Node;
		int32_t L_1;
		L_1 = IntPtr_op_Explicit_mC33775570B5AC86421DABA8F8B9885DBFF49B02F(L_0, NULL);
		int32_t L_2 = __this->___Version;
		V_0 = ((int32_t)(((int32_t)il2cpp_codegen_multiply(L_1, ((int32_t)397)))^L_2));
		goto IL_001d;
	}

IL_001d:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/AudioHandle.bindings.cs:81>
		int32_t L_3 = V_0;
		return L_3;
	}
}
IL2CPP_EXTERN_C  int32_t Handle_GetHashCode_m8DD5E2EE5197C1FF37CD2CF3B4FD892A9E299DFE_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153*>(__this + _offset);
	int32_t _returnValue;
	_returnValue = Handle_GetHashCode_m8DD5E2EE5197C1FF37CD2CF3B4FD892A9E299DFE(_thisAdjusted, method);
	return _returnValue;
}
// Method Definition Index: 87557
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Handle_CheckValidOrThrow_mA6C3BAA711CDA462A6A2653A1A2D04CE2AECD3C3 (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* __this, const RuntimeMethod* method) 
{
	bool V_0 = false;
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/AudioHandle.bindings.cs:85>
		bool L_0;
		L_0 = Handle_get_ValidAndNotDisposed_m1097F1E099632EDF74478AD5B74978BD3FAEF145(__this, NULL);
		V_0 = (bool)((((int32_t)L_0) == ((int32_t)0))? 1 : 0);
		bool L_1 = V_0;
		if (!L_1)
		{
			goto IL_0019;
		}
	}
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/AudioHandle.bindings.cs:86>
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_2 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var)));
		InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162(L_2, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral23ABA34B45DF51C7D9C0E116AA6FDA4749E8C618)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_2, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Handle_CheckValidOrThrow_mA6C3BAA711CDA462A6A2653A1A2D04CE2AECD3C3_RuntimeMethod_var)));
	}

IL_0019:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/AudioHandle.bindings.cs:87>
		return;
	}
}
IL2CPP_EXTERN_C  void Handle_CheckValidOrThrow_mA6C3BAA711CDA462A6A2653A1A2D04CE2AECD3C3_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153*>(__this + _offset);
	Handle_CheckValidOrThrow_mA6C3BAA711CDA462A6A2653A1A2D04CE2AECD3C3(_thisAdjusted, method);
}
// Method Definition Index: 87558
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Handle_get_ValidAndNotDisposed_m1097F1E099632EDF74478AD5B74978BD3FAEF145 (Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* __this, const RuntimeMethod* method) 
{
	int32_t G_B4_0 = 0;
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/AudioHandle.bindings.cs:89>
		intptr_t L_0 = __this->___m_Node;
		bool L_1;
		L_1 = IntPtr_op_Inequality_m90EFC9C4CAD9A33E309F2DDF98EE4E1DD253637B_inline(L_0, 0, NULL);
		if (!L_1)
		{
			goto IL_0035;
		}
	}
	{
		Node_tE9DD997AC2A1CDC0DF9579D3B88467E7F42142C8* L_2;
		L_2 = Handle_get_AtomicNode_m3D7AF7852BDF5999877F8F973FDF8724974B0A50(__this, NULL);
		NullCheck(L_2);
		int32_t L_3 = L_2->___Version;
		int32_t L_4 = __this->___Version;
		if ((!(((uint32_t)L_3) == ((uint32_t)L_4))))
		{
			goto IL_0035;
		}
	}
	{
		Node_tE9DD997AC2A1CDC0DF9579D3B88467E7F42142C8* L_5;
		L_5 = Handle_get_AtomicNode_m3D7AF7852BDF5999877F8F973FDF8724974B0A50(__this, NULL);
		NullCheck(L_5);
		int32_t L_6 = L_5->___AllocationFlags;
		G_B4_0 = ((((int32_t)L_6) == ((int32_t)0))? 1 : 0);
		goto IL_0036;
	}

IL_0035:
	{
		G_B4_0 = 0;
	}

IL_0036:
	{
		return (bool)G_B4_0;
	}
}
IL2CPP_EXTERN_C  bool Handle_get_ValidAndNotDisposed_m1097F1E099632EDF74478AD5B74978BD3FAEF145_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<Handle_t5E550C52101EDD3CC66477987E73F87CCCFD5153*>(__this + _offset);
	bool _returnValue;
	_returnValue = Handle_get_ValidAndNotDisposed_m1097F1E099632EDF74478AD5B74978BD3FAEF145(_thisAdjusted, method);
	return _returnValue;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
// Method Definition Index: 87488
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AudioConfigurationChangeHandler_Invoke_m4DC27DD11512481B60071B20284E6886DAE54DE2_inline (AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177* __this, bool ___0_deviceWasChanged, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	typedef void (*FunctionPointerType) (RuntimeObject*, bool, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_deviceWasChanged, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
// Method Definition Index: 12713
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_Invoke_m7126A54DACA72B845424072887B5F3A51FC3808E_inline (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	typedef void (*FunctionPointerType) (RuntimeObject*, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
// Method Definition Index: 87489
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Mobile_get_muteState_m64C1E8C61537317A7F153E1A72F7D39D85DA684D_inline (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mobile_t304A73480DF447472BDB16BA19A9E4FE2C8CB2DD_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:459>
		bool L_0 = ((Mobile_t304A73480DF447472BDB16BA19A9E4FE2C8CB2DD_StaticFields*)il2cpp_codegen_static_fields_for(Mobile_t304A73480DF447472BDB16BA19A9E4FE2C8CB2DD_il2cpp_TypeInfo_var))->___U3CmuteStateU3Ek__BackingField;
		return L_0;
	}
}
// Method Definition Index: 87490
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Mobile_set_muteState_m7C9A464BCA3762330E18CCAD79AF6C47B863CA02_inline (bool ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mobile_t304A73480DF447472BDB16BA19A9E4FE2C8CB2DD_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptBindings/Audio.bindings.cs:460>
		bool L_0 = ___0_value;
		((Mobile_t304A73480DF447472BDB16BA19A9E4FE2C8CB2DD_StaticFields*)il2cpp_codegen_static_fields_for(Mobile_t304A73480DF447472BDB16BA19A9E4FE2C8CB2DD_il2cpp_TypeInfo_var))->___U3CmuteStateU3Ek__BackingField = L_0;
		return;
	}
}
// Method Definition Index: 87504
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void PCMReaderCallback_Invoke_m76784C690C36B513E2AA5B0E4FD9831B2C7E5152_inline (PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* __this, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* ___0_data, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	typedef void (*FunctionPointerType) (RuntimeObject*, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C*, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_data, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
// Method Definition Index: 87506
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void PCMSetPositionCallback_Invoke_m434D4F02FA25F91DF6199EC5A799C551C7F93702_inline (PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* __this, int32_t ___0_position, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	typedef void (*FunctionPointerType) (RuntimeObject*, int32_t, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_position, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
// Method Definition Index: 87526
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_inline (SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* __this, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2* ___0_provider, uint32_t ___1_sampleFrameCount, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	typedef void (*FunctionPointerType) (RuntimeObject*, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2*, uint32_t, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_provider, ___1_sampleFrameCount, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
// Method Definition Index: 15447
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool IntPtr_op_Inequality_m90EFC9C4CAD9A33E309F2DDF98EE4E1DD253637B_inline (intptr_t ___0_value1, intptr_t ___1_value2, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = ___0_value1;
		intptr_t L_1 = ___1_value2;
		return (bool)((((int32_t)((((intptr_t)L_0) == ((intptr_t)L_1))? 1 : 0)) == ((int32_t)0))? 1 : 0);
	}
}
// Method Definition Index: 18730
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool GCHandle_get_IsAllocated_m241908103D8D867E11CCAB73C918729825E86843_inline (GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = __this->___handle;
		bool L_1;
		L_1 = IntPtr_op_Inequality_m90EFC9C4CAD9A33E309F2DDF98EE4E1DD253637B_inline(L_0, 0, NULL);
		return L_1;
	}
}
// Method Definition Index: 87543
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Configuration_get_isRealtime_m99E988C200947A8277C144A84CB1755B5A0C2D78_inline (Configuration_t6237CF0DD0C43C64808F31B712BA308606D3F060* __this, const RuntimeMethod* method) 
{
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ScriptableGenerator.bindings.cs:255>
		bool L_0 = __this->___IsRealtime;
		return L_0;
	}
}
// Method Definition Index: 87536
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 GeneratorInstance_op_Implicit_mB7A6E46130D3BAB42E00F4DD08155EF286A16167_inline (GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2* ___0_generatorInstance, const RuntimeMethod* method) 
{
	{
		//<source_info:/home/bokken/build/output/unity/unity/Modules/Audio/Public/ScriptableProcessors/ScriptBindings/ScriptableGenerator.bindings.cs:392>
		GeneratorInstance_t2721F6826DF0F07285CF2F765C273DFD4D8201C2* L_0 = ___0_generatorInstance;
		ProcessorInstance_tC8E3131DB14380822C20BA430BC6BFB4BE0F8B35 L_1 = L_0->___m_ProcessorInstance;
		return L_1;
	}
}
// Method Definition Index: 41558
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool DiscreteTime_op_Inequality_m4C12F6A54E5C6E8BAC45A3903E6C2A3127C6063C_inline (DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E ___0_lhs, DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E ___1_rhs, const RuntimeMethod* method) 
{
	bool V_0 = false;
	{
		//<source_info:/home/bokken/build/output/unity/unity/Runtime/Export/Time/DiscreteTime.cs:158>
		DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E L_0 = ___0_lhs;
		int64_t L_1 = L_0.___Value;
		DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E L_2 = ___1_rhs;
		int64_t L_3 = L_2.___Value;
		V_0 = (bool)((((int32_t)((((int64_t)L_1) == ((int64_t)L_3))? 1 : 0)) == ((int32_t)0))? 1 : 0);
		goto IL_0015;
	}

IL_0015:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Runtime/Export/Time/DiscreteTime.cs:158>
		bool L_4 = V_0;
		return L_4;
	}
}
// Method Definition Index: 15446
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline (intptr_t ___0_value1, intptr_t ___1_value2, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = ___0_value1;
		intptr_t L_1 = ___1_value2;
		return (bool)((((intptr_t)L_0) == ((intptr_t)L_1))? 1 : 0);
	}
}
// Method Definition Index: 12715
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_m69C8773D6967F3B224777183E24EA621CE056F8F_gshared_inline (Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C* __this, bool ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	typedef void (*FunctionPointerType) (RuntimeObject*, bool, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_obj, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
// Method Definition Index: 44920
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR intptr_t MarshalledUnityObject_MarshalNotNull_TisRuntimeObject_mEB1AA6B672D00242BB9DCE007056EC0E9C8DB075_gshared_inline (RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	intptr_t V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		//<source_info:/home/bokken/build/output/unity/unity/Runtime/Export/Scripting/UnityEngineObject.bindings.cs:1000>
		RuntimeObject* L_0 = ___0_obj;
		NullCheck(L_0);
		intptr_t L_1 = ((Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)L_0)->___m_CachedPtr;
		V_0 = L_1;
		goto IL_000f;
	}

IL_000f:
	{
		//<source_info:/home/bokken/build/output/unity/unity/Runtime/Export/Scripting/UnityEngineObject.bindings.cs:1002>
		intptr_t L_2 = V_0;
		return L_2;
	}
}
// Method Definition Index: 13821
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Nullable_1_get_HasValue_m2C09C399366A16930BBC996441FE7CED2907D3D0_gshared_inline (Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		bool L_0 = __this->___hasValue;
		return L_0;
	}
}
// Method Definition Index: 13823
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E Nullable_1_GetValueOrDefault_m5879DF28E8CBC03BD7B2F3A7B383BB0068DE64DC_gshared_inline (Nullable_1_t9EF99F82DC3D0A8DFF605D72E3793599BE02E363* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		DiscreteTime_t0954C3B93A9A500A04D33E309649C1CD2780C41E L_0 = __this->___value;
		return L_0;
	}
}
