using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Опанасенко
{
    public enum Group { Ремонт , Косметика , Одяг , Продукти }

    struct Sale
    {
        public int id;
        public Group group;
        public string product;
        public DateTime date;
        public int count;
        public int price;

        public Sale(int id, Group group, string product, DateTime date, int count, int price)
        {
            this.id = id;
            this.group = group;
            this.product = product;
            this.date = date;
            this.count = count;
            this.price = price;
        }
    }

    public partial class Form1 : Form
    {
        Sale[] sales;
        public Form1()
        {

            InitializeComponent();
            edit_product.Enabled = false;
            search.Text = String.Empty;
            edit_group_combo.Enabled = false;
            edit_dateTimePicker.Enabled = false;
            edit_count.Enabled = false;
            edit_price.Enabled = false;
            sales = new Sale[]
            {
                new Sale(1,Group.Косметика,"Крем",new DateTime(2020,10,01),10,150),
                new Sale(2,Group.Продукти,"Банан",new DateTime(2020,12,01),15,540),
                new Sale(3,Group.Ремонт,"Шпатель",new DateTime(2020,11,01),55,540),
                new Sale(4,Group.Ремонт,"Фарба",new DateTime(2020,09,01),5,340),
                new Sale(5,Group.Одяг,"Куртка",new DateTime(2020,08,01),30,20),
                new Sale(6,Group.Одяг,"Штани",new DateTime(2020,07,01),46,450),
                new Sale(7,Group.Одяг,"Футболка",new DateTime(2020,06,01),84,50),
                new Sale(8,Group.Продукти,"Яблуко",new DateTime(2020,05,01),52,80),
                new Sale(9,Group.Косметика,"Бальзам",new DateTime(2020,04,01),145,5450),
                new Sale(10,Group.Продукти,"Шоколад",new DateTime(2020,01,01),185,1750),
                new Sale(11,Group.Косметика,"Скраб",new DateTime(2020,03,01),8,890)
            };
            OutPut(sales, dataGridView1);
        }
        static void OutPut(Sale[] sales, DataGridView dgv)
        {
            dgv.RowCount = sales.Length;
            for (int i = 0; i < dgv.RowCount; i++)
            {
                int count = 0;
                dgv[count++, i].Value = sales[i].id.ToString();
                dgv[count++, i].Value = sales[i].group.ToString();
                dgv[count++, i].Value = sales[i].product.ToString();
                dgv[count++, i].Value = sales[i].date.ToShortDateString();
                dgv[count++, i].Value = sales[i].count.ToString();
                dgv[count++, i].Value = sales[i].price.ToString();
            }
        }

        private void add_btn_Click(object sender, EventArgs e)
        {
            try
            {
                int num = sales[sales.Length - 1].id + 1;
                int.TryParse(add_count.Text, out int count);
                int.TryParse(add_price.Text, out int price);
                int.TryParse(add_group_combo.SelectedItem.ToString().Substring(0, 1), out int group);
                string product = add_product_textbox.Text;
                DateTime date = add_dateTimePicker1.Value;
                sales = Add(sales, new Sale(num, (Group)group, product,date, count, price));
                OutPut(sales, dataGridView1);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private Sale[] Add(Sale[] sale, Sale newSale)
        {
            Sale[] newSales = new Sale[sale.Length + 1];
            for (int i = 0; i < sale.Length; i++)
            {
                newSales[i] = sale[i];
            }
            newSales[newSales.Length - 1] = newSale;
            return newSales;

        }
        private Sale[] Delete(Sale[] sale, int id)
        {
            Sale[] newSales = new Sale[sale.Length - 1];
            int count_ex = 0;
            for (int i = 0; i < sale.Length; i++)
            {
                if (sale[i].id == id)
                {
                    continue;
                }
                else
                {
                    newSales[count_ex++] = sale[i];
                }
            }
            return newSales;
        }
       
        private void del_btn_Click(object sender, EventArgs e)
        {
            try
            {
                int.TryParse(search_delete.Text, out int id);
                sales = Delete(sales, id);
                OutPut(sales, dataGridView1);
                search_delete.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            if (int.TryParse(search.Text, out int id))
            {
                bool temp = false;
                Sale sale = new Sale();

                for (int i = 0; i < sales.Length; i++)
                {
                    if (id == sales[i].id)
                    {
                        temp = true;
                        sale = sales[i];
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                if (temp)
                {
                    button_search.Enabled = false;
                    search.Enabled = false;
                    edit_count.Enabled = true;
                    edit_dateTimePicker.Enabled = true;
                    edit_group_combo.Enabled = true;
                    edit_price.Enabled = true;
                    edit_product.Enabled = true;
                    edit_btn.Enabled = true;
                    edit_count.Text = sale.count.ToString();
                    edit_product.Text = sale.product.ToString();
                    int fac = (int)sale.group;
                    edit_group_combo.Text = fac + " - " + sale.group.ToString();
                    edit_dateTimePicker.Value = sale.date;
                    edit_price.Text = sale.price.ToString();
                }
            }
        }

        private void edit_btn_Click(object sender, EventArgs e)
        {
            int id = int.Parse(search.Text);
            for (int i = 0; i < sales.Length; i++)
            {
                if (sales[i].id == id)
                {
                    int.TryParse(edit_count.Text, out int count);
                    int.TryParse(edit_price.Text, out int price);
                    sales[i].count = count;
                    sales[i].product = edit_product.Text;
                    sales[i].date = edit_dateTimePicker.Value;
                    sales[i].price = price;
                    try
                    {
                        sales[i].group= (Group)int.Parse(edit_group_combo.Text.Substring(0, 1));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            edit_product.Enabled = false;
            search.Text = String.Empty;
            edit_group_combo.Enabled = false;
            edit_dateTimePicker.Enabled = false;
            edit_count.Enabled = false;
            edit_price.Enabled = false;
            edit_btn.Enabled = false;
            button_search.Enabled = true;
            search.Enabled = true;
            edit_price.Clear();
            edit_count.Clear();
            edit_product.Text = String.Empty;
            edit_group_combo.Text = String.Empty;
            edit_dateTimePicker.Value = DateTime.Now;
            OutPut(sales, dataGridView1);
        }


        private void button_task1_Click(object sender, EventArgs e)
        {
            int input = comboBox_task1.SelectedIndex;
            DateTime zz = dateTimePicker_task1_1.Value;
            DateTime doo = dateTimePicker_task1_2.Value;

            Sale[] sale_rez = new Sale[0];
            for (int i = 0; i < sales.Length; i++)
            {
                if ((int)sales[i].group == input &&
                        sales[i].date >= zz &&
                        sales[i].date <= doo)
                {
                    Array.Resize(ref sale_rez, sale_rez.Length + 1);
                    sale_rez[sale_rez.Length - 1] = sales[i];
                }
            }

            for (int i = 0; i < sale_rez.Length; i++)
            {
                for (int j = i; j < sale_rez.Length; j++)
                {
                    if (sale_rez[i].date > sale_rez[j].date)
                    {
                        Sale temp = sale_rez[j];
                        sale_rez[j] = sale_rez[i];
                        sale_rez[i] = temp;
                    }
                }
            }
            OutPut(sale_rez, dataGridView_task1);

        }

        private void button_task3_Click(object sender, EventArgs e)
        {
            string input = textBox_task3.Text;

            Sale[] outputArr = new Sale[0];

            for (int i = 0; i < sales.Length; i++)
            {
                if (sales[i].product == input)
                {
                    Array.Resize(ref outputArr, outputArr.Length + 1);
                    outputArr[outputArr.Length - 1] = sales[i];
                }
            }
            OutPut(outputArr, dataGridView_task3);
        }

        private void button_task2_Click(object sender, EventArgs e)
        {
            string input_produkt = textBox_task2.Text;

            DateTime zz = dateTimePicker_task2_1.Value;
            DateTime doo = dateTimePicker_task2_2.Value;

            double output = 0;
            for (int i = 0; i < sales.Length; i++)
            {
                if (sales[i].date >= zz && sales[i].date <= doo && sales[i].product == input_produkt)
                {
                    double temp = sales[i].price * sales[i].count;
                    output += temp;
                }
            }

            label_task2.Text = output + " гривень";
        }

        private void button_task4_Click(object sender, EventArgs e)
        {
            DateTime zz = dateTimePicker_task4_1.Value;
            DateTime doo = dateTimePicker_task4_2.Value;
            if (zz > doo)
            {
                MessageBox.Show("Дати введено не коректно!");
            }
            else
            {
                DateTime[] dates = new DateTime[0];

                DateTime temp_start = zz;
                while (temp_start <= doo)
                {
                    Array.Resize(ref dates, dates.Length + 1);
                    dates[dates.Length - 1] = temp_start;
                    temp_start = temp_start.AddDays(1);
                }
                Array.Resize(ref dates, dates.Length + 1);
                dates[dates.Length - 1] = doo;

                double[] viruchka = new double[dates.Length];
                for (int i = 0; i < dates.Length; i++)
                {
                    double t = 0;
                    for (int j = 0; j < sales.Length; j++)
                    {
                        if (dates[i].ToShortDateString() == sales[j].date.ToShortDateString())
                        {
                            t += sales[j].price * sales[j].count;
                        }
                    }
                    viruchka[i] = t;
                }

                dataGridView_task4.RowCount = dates.Length;
                for (int i = 0; i < dataGridView_task4.RowCount; i++)
                {
                    int count = 0;
                    dataGridView_task4[count++, i].Value = dates[i].ToShortDateString();
                    dataGridView_task4[count++, i].Value = viruchka[i].ToString() + " грн";
                }
            }
        }
    }

}
