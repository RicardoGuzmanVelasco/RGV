﻿using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace RGV.DesignByContract.Runtime
{
    public static partial class Contract
    {
        [AssertionMethod, DebuggerStepThrough, DebuggerHidden]
        public static Contract<bool> False(this Contract<bool> contract)
        {
            contract.Not.True();
            return contract;
        }

        [AssertionMethod, DebuggerStepThrough, DebuggerHidden]
        public static Contract<bool> True(this Contract<bool> contract)
        {
            contract.Evaluate<ArgumentException>
            (
                b => b
            );
            return contract;
        }
    }
}