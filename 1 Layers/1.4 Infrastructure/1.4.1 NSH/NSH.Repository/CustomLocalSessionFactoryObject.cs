using System;
using System.IO;
using NHibernate.Bytecode;
using Spring.Data.NHibernate;
using Spring.Data.NHibernate.Bytecode;

namespace NSH.Repository
{
    /// <summary>
    /// A custom version of <see cref="LocalSessionFactoryObject" /> that sets 
    /// bytecode provider to be Spring.NET's <see cref="BytecodeProvider" />.
    /// </summary>
    public class CustomLocalSessionFactoryObject : LocalSessionFactoryObject
    {
        /// <summary>
        /// Overwritten to return Spring's bytecode provider for entity injection to work.
        /// </summary>
        public override IBytecodeProvider BytecodeProvider
        {
            get { return new BytecodeProvider(ApplicationContext); }
        }

        public override void AfterPropertiesSet()
        {
            if (this.HibernateProperties != null
                && this.HibernateProperties.Contains("hbm2ddl.auto")
                && (String.IsNullOrEmpty(this.HibernateProperties["hbm2ddl.auto"].ToString())
                || this.HibernateProperties["hbm2ddl.auto"].ToString() == "none"))
            {
                this.HibernateProperties.Remove("hbm2ddl.auto");
            }
            base.AfterPropertiesSet();
        }
    }
}
