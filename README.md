# SrcGen_Enum_ToEnumMethods
### 같은 이름에 값만 다른 두 enum타입에 대해 변환 처리가 필요할 때 사용
    
    enum values     enum flags      public static flags ConvertToflags(values value)
    {               {               {
        x = 1,          x = 0x01,       switch(value)
        y = 2,          y = 0x02,       {
    }               }                   case ...:
                                        }
                                    }
                                    
# SrcGen_Enum_JoinEnums
### 여러 개의 Enum 을 합친 Enum 을 정의하기 위해 사용
     
    enum A      enum B      enum C
    {           {           {
        a,          b,          a,
                                b,
    }           }

# SrcGen_Method_GenericParamInc
### Generic 함수의 매개변수가 추가된 오버로딩 함수 추가시 사용
    
    void Run<T1>(T1 t1)     void Run<T1,T2>(T1 t1, T2 t2)       void Run<T1,T2,T3>(T1 t1, T2 t2, T3 t2)
    {                       {                                   {
        do1(t1);                do1(t1,t2);                         do1(t1,t2,t3);
        do2(t1);                do2(t1);                            do2(t1);
                                do2(t2);                            do2(t2);
                                                                    do2(t3);
    }                       }                                   }

# SrcGen_Class_AddMemberUsingEnum
### Enum 을 사용해서 클래스 멤버 변수를 선언할 때 모든 enum에 대해 동일하게 적용
    
    enum E                  class A                     class A
    {                       {                           {
        e1,                     int e1 = E.e1;              int e1 = E.e1;
        e2,                                                 int e2 = E.e2;
        e3,                                                 int e3 = E.e3;
    }                       }                           }
