using System;
using System.Transactions;
using PostSharp.Aspects;

namespace MRTFramework.CrossCuttingConcern.AspectOrientedProgramming.PostSharp.TransactionAspect
{
    [Serializable]
    public class TransactionScopeAspect : OnMethodBoundaryAspect
    {
        private readonly TransactionScopeOption _scopeOption;
        private readonly IsolationLevel _isolationLevel;
        private readonly uint _timeout;

        public TransactionScopeAspect()
        {

        }
        public TransactionScopeAspect(TransactionScopeOption scopeOption)
        {
            _scopeOption = scopeOption;
        }

        public TransactionScopeAspect(TransactionScopeOption scopeOption, IsolationLevel isolationLevel, uint timeout)
        {
            _isolationLevel = isolationLevel;
            _timeout = timeout;
            _scopeOption = scopeOption;
        }


        public override void OnEntry(MethodExecutionArgs args)
        {

            if (_timeout > 0)
            {
                var isolationLevelOption = new TransactionOptions()
                {
                    IsolationLevel = _isolationLevel,
                    Timeout = TimeSpan.FromSeconds(_timeout)
                };

                args.MethodExecutionTag = new TransactionScope(_scopeOption, isolationLevelOption);
            }
            else
            {
                args.MethodExecutionTag = new TransactionScope(_scopeOption);
            }

        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            ((TransactionScope)args.MethodExecutionTag).Complete();
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            ((TransactionScope)args.MethodExecutionTag).Dispose();
        }
    }
}
