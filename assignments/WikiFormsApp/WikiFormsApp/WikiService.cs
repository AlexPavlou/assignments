using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiFormsApp
{
    public partial class WikiService : Component
    {
        public WikiService()
        {
            InitializeComponent();
        }

        public WikiService(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
