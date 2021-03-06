﻿using System;
using System.Dynamic;

namespace AmplaData.Dynamic.Methods.Strategies
{
    public class Binder
    {
        private string Name { get; set; }
        private Type BinderType { get; set; }

        private string[] NamedArgs { get; set; }

        private int ArgumentCount { get; set; }

        public object[] Args { get; set; }

        public InvokeMemberBinder GetMemberBinder()
        {
            CallInfo callInfo = new CallInfo(ArgumentCount, NamedArgs);
            return new TestInvokeMemberBinder(Name, callInfo);
        }

        public static InvokeMemberBinder GetMemberBinder(string name, int argCount, params string[] namedArgs)
        {
            CallInfo callInfo = new CallInfo(argCount, namedArgs);
            return new TestInvokeMemberBinder(name, callInfo);
        }

        public static GetIndexBinder GetIndexBinder(int argCount, params string[] namedArgs)
        {
            CallInfo callInfo = new CallInfo(argCount, namedArgs);
            return new TestGetIndexBinder(callInfo);
        }

        private class TestInvokeMemberBinder : InvokeMemberBinder
        {
            public TestInvokeMemberBinder(string name, CallInfo callInfo) : base(name, false, callInfo)
            {
            }

            public override DynamicMetaObject FallbackInvokeMember(DynamicMetaObject target, DynamicMetaObject[] args,
                                                                   DynamicMetaObject errorSuggestion)
            {
                throw new NotImplementedException();
            }

            public override DynamicMetaObject FallbackInvoke(DynamicMetaObject target, DynamicMetaObject[] args,
                                                             DynamicMetaObject errorSuggestion)
            {
                throw new NotImplementedException();
            }
        }

        private class TestGetIndexBinder : GetIndexBinder
        {
            public TestGetIndexBinder(CallInfo callInfo) : base(callInfo)
            {
            }

            public override DynamicMetaObject FallbackGetIndex(DynamicMetaObject target, DynamicMetaObject[] indexes,
                                                               DynamicMetaObject errorSuggestion)
            {
                throw new NotImplementedException();
            }
        }

        public static Binder Member
        {
            get
            {
                Binder binder = new Binder {BinderType = typeof (InvokeMemberBinder), Name = "Method"};
                return binder;
            }
        }

        public Binder Named(string binderName)
        {
            Name = binderName;
            return this;
        }

        public Binder WithArguments(int argumentCount, params string[] namedArgs)
        {
            ArgumentCount = argumentCount;
            NamedArgs = namedArgs;

            return this;
        }


        public Binder Passed(params object[] args)
        {
            Args = args;
            return this;
        }

    }
}