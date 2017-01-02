using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccess;
namespace EF_Tutorials
{
    public partial class Form1 : Form
    {

        EFEntities efEntities = new EFEntities();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Add")
            {
                efEntities.Employees.Add(new Employee() { ID = 1, Name = "Naresh", Sex = "Male", Age = 25, DOB = DateTime.Now.AddYears(-32), DOJ = DateTime.Now.AddYears(-4) });
                efEntities.SaveChanges();
                button1.Text = "Attach";
            }
            else if (button1.Text == "Attach")
            {
                efEntities.Employees.Attach(new Employee() {  Name = "Mahesh", Sex = "Male", Age = 25, DOB = DateTime.Now.AddYears(-32), DOJ = DateTime.Now.AddYears(-4) });
                efEntities.SaveChanges();
                button1.Text = "Add";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
             var list=  efEntities.Employees.AsNoTracking().ToList();
             MessageBox.Show(list.Count.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
           var item= efEntities.Employees.SqlQuery("SELECT * FROM EMPLOYEE WHERE ID=5").FirstOrDefault();
           MessageBox.Show(item.Name.ToString());

           item.Name = "Naresh Kumar Dhuliya";

           var entity = efEntities.Entry(item);

           MessageBox.Show(entity.Entity.GetType().Name.ToString());

           MessageBox.Show(entity.State.ToString());
           foreach (var propertyName in entity.CurrentValues.PropertyNames)
           {
               MessageBox.Show("Property Name: {0}", propertyName);

               //get original value
               var orgVal = entity.OriginalValues[propertyName];
               MessageBox.Show("     Original Value: {0}" + orgVal);

               //get current values
               var curVal = entity.CurrentValues[propertyName];
               MessageBox.Show("     Current Value: {0}"+ curVal);
           }


        }



    }
}
