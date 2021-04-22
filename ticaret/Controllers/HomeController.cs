using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using ticaret.App_Classes;
using ticaret.Models;


namespace ticaret.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            
            return View();
        }
        public PartialViewResult Sepet()
        {
            return PartialView();
        }

        public PartialViewResult MiniSepetWidget()
        {
            if (HttpContext.Session["AktifSepet"] != null)
                return PartialView((sepet)HttpContext.Session["AktifSepet"]);
            else
                return PartialView();

        }
        public PartialViewResult Slider(Resim model)
        {
            return PartialView(data(model));
        }



        public IEnumerable<Resim> data(Resim model)
        {
            using (var gr_con1 = new SqlConnection(WebConfigurationManager.ConnectionStrings["TSS"].ConnectionString))
            {
                if (gr_con1.State == ConnectionState.Closed)
                    gr_con1.Open();


                SqlCommand MyCommand22 = new SqlCommand("", gr_con1);
                MyCommand22.CommandText = "SELECT  * FROM Resim";

                SqlDataReader rd1 = MyCommand22.ExecuteReader();
                if (rd1.HasRows)
                {

                    while (rd1.Read())
                    {

                        yield return new Resim()
                        {

                            BuyukYol = (string)rd1["BuyukYol"],









                        };
                    }

                }



                if (gr_con1.State == ConnectionState.Open)
                    gr_con1.Close();
            }
        }
        public PartialViewResult Markalar(Marka model)
        {
            return PartialView(marka_data(model));
        }

        public IEnumerable<Marka> marka_data(Marka model)
        {

            using (var gr_con1 = new SqlConnection(WebConfigurationManager.ConnectionStrings["TSS"].ConnectionString))
            {
                if (gr_con1.State == ConnectionState.Closed)
                    gr_con1.Open();


                SqlCommand MyCommand22 = new SqlCommand("", gr_con1);
                MyCommand22.CommandText = "SELECT  * FROM Marka";
                model.sonuc = MyCommand22.ToString();
                SqlDataReader rd1 = MyCommand22.ExecuteReader();
                if (rd1.HasRows)
                {

                    while (rd1.Read())
                    {

                        yield return new Marka()
                        {
                            Id = (int)rd1["marka_id"],
                            Adi = (string)rd1["marka_adi"],
                            resim = (string)rd1["resim"],








                        };
                    }

                }



                if (gr_con1.State == ConnectionState.Open)
                    gr_con1.Close();

            }
        }

        public PartialViewResult YeniUrunler(urunler model)
        {
            return PartialView(yeniurun_data(model));
        }
        public IEnumerable<urunler> yeniurun_data(urunler model)
        {
            using (var gr_con1 = new SqlConnection(WebConfigurationManager.ConnectionStrings["TSS"].ConnectionString))
            {
                if (gr_con1.State == ConnectionState.Closed)
                    gr_con1.Open();


                SqlCommand MyCommand22 = new SqlCommand("", gr_con1);
                MyCommand22.CommandText = "SELECT  * FROM Urunler";

                SqlDataReader rd1 = MyCommand22.ExecuteReader();
                if (rd1.HasRows)
                {

                    while (rd1.Read())
                    {

                        yield return new urunler()
                        {

                            Id = (int)rd1["urun_id"],
                            Adi = (string)rd1["urun_adi"],
                            ozellık = (string)rd1["ozellık"],
                            Fiyat = (decimal)rd1["fiyat"],
                            beden = (string)rd1["beden"],
                            resim = (string)rd1["resim"],






                        };
                    }

                }



                if (gr_con1.State == ConnectionState.Open)
                    gr_con1.Close();
            }
        }
        public void SepeteEkle(int id, urunler model)
        {
            SepetItem si = new SepetItem();
            using (var gr_con = new SqlConnection(WebConfigurationManager.ConnectionStrings["TSS"].ConnectionString))
            {
                if (gr_con.State == ConnectionState.Closed)
                    gr_con.Open();
                SqlCommand MyCommand22 = new SqlCommand("", gr_con);
                MyCommand22.CommandText = "SELECT top 1 *  FROM Urunler where urun_id= '" + id + "'";
                SqlDataReader rdr2 = MyCommand22.ExecuteReader();

                // si.Urun =rdr2;
                si.Adet = 1;
                si.Indirim = 0;
                sepet s = new sepet();
                s.SepeteEkle(si);
                if (rdr2.HasRows)
                {
                    while (rdr2.Read())
                    {
                        model.Id = (int)rdr2["urun_id"];

                        // model.soyad = (string)rdr2["alan2"];
                    }
                }
            }




        }
        public PartialViewResult Servisler()
        {
            return PartialView();
        }

        public ActionResult UrunDetay(urunler model)
        {
            return PartialView(urun_data(model));
        }
        public IEnumerable<urunler> urun_data(urunler model)
        {
            using (var gr_con1 = new SqlConnection(WebConfigurationManager.ConnectionStrings["TSS"].ConnectionString))
            {
                if (gr_con1.State == ConnectionState.Closed)
                    gr_con1.Open();



                SqlCommand MyCommand22 = new SqlCommand("", gr_con1);
                MyCommand22.CommandText = "SELECT  * FROM Urunler where urun_id='" + model.Id + "'";

                SqlDataReader rd1 = MyCommand22.ExecuteReader();
                if (rd1.HasRows)
                {

                    while (rd1.Read())
                    {

                        yield return new urunler()
                        {

                            Id = (int)rd1["urun_id"],
                            Adi = (string)rd1["urun_adi"],
                            ozellık = (string)rd1["ozellık"],
                            Fiyat = (decimal)rd1["fiyat"],
                            beden = (string)rd1["beden"],
                            resim = (string)rd1["resim"],





                        };
                    }

                }



                if (gr_con1.State == ConnectionState.Open)
                    gr_con1.Close();
            }






        }


        public ActionResult login(login model)
        {

            if (model.Command == "giris")
            {

                try
                {
                    SqlConnection gr_con = new SqlConnection(WebConfigurationManager.ConnectionStrings["TSS"].ConnectionString);
                    if (gr_con.State == ConnectionState.Closed)
                        gr_con.Open();
                    SqlCommand komut = new SqlCommand("", gr_con);



                    komut.CommandText = "select * from Kullanıcı  where kullanici_adi = '" + model.kullanici_adi + "' AND sifre = '" + model.sifre + "'";
                    SqlDataReader rd = komut.ExecuteReader();



                    if (rd.Read())
                    {

                       
                         //   Session["giris"] = "okey"; //session oluşturma
                           // Session["k_adi"] = rd["kullaniciadi"].ToString();
                            //iewBag.ad = Session["k_adi"].ToString();
                        //ViewBag.ad = Session["kadi"].ToString();
                        // Session["giris"] = "okey"; //session oluşturma
                        //  Session["k_id"] = rd["k_id"].ToString();
                        //   model.id = Session["k_id"].ToString();


                        //Session["kadi"] = model.kullanici_adi;

                        if (rd["yetki_id"].ToString() == "1")
                        {
                           

                            Response.Redirect("/home/Index");



                        }
                        else
                        {
                            Response.Redirect("/admin/Index");


                        }



                    }
                    //  ViewBag.donus = "Giriş Başarılı";
                    //  Response.Redirect("/home/ekle");
                    //Session["giris"] = "okey"; //session oluşturma
                    //Session["k_adi"] = model.kullanici_adi;
                    // Session["k_id"] = model.id.ToString();






                    else
                    {

                        ViewBag.donus = "Kullanıcı veya sifre yanlış";
                    }

                    if (gr_con.State == ConnectionState.Open)
                        gr_con.Close();
                }

                catch
                {


                }


            }
            return View();


        }



        public ActionResult loginkayit(login model)
        {
            if (model.Command == "giris")
            {

                /*Random rastgele = new Random();
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i <= 4; i++)
                {
                    int ascii = rastgele.Next(65, 91);
                    char karakter = Convert.ToChar(ascii);
                    sb.Append(karakter);



                }*/

                try
                {
                    SqlConnection gr_con = new SqlConnection(WebConfigurationManager.ConnectionStrings["TSS"].ConnectionString);
                    if (gr_con.State == ConnectionState.Closed)
                        gr_con.Open();
                    string kayit = "insert into Kullanıcı (isim,soyisim,tc,e_posta,tel,kullanici_adi,sifre) values (@alan1,@alan2,@alan3,@alan4,@alan5,@alan6,@alan7)";
                    SqlCommand komut = new SqlCommand(kayit, gr_con);
                    if (ModelState.IsValid)
                    {

                        var body = new StringBuilder();

                        komut.Parameters.AddWithValue("@alan1", model.isim);
                        komut.Parameters.AddWithValue("@alan2", model.soyisim);
                        komut.Parameters.AddWithValue("@alan3", model.tc);
                        komut.Parameters.AddWithValue("@alan4", model.email);
                        komut.Parameters.AddWithValue("@alan5", model.tel);
                        komut.Parameters.AddWithValue("@alan6", model.kullanici_adi);
                        komut.Parameters.AddWithValue("@alan7", model.sifre);

                        //   komut.Parameters.AddWithValue("@alan8", sb.ToString());





                        // Session["giris"] = "okey"; //session oluşturma
                        //  Session["kod"] = sb.ToString();

                        // Gmail.SendMail(sb.ToString());
                        //  ViewBag.Success = true;
                        //ViewBag.kod = sb.ToString();






                    }


                    int rowsAffected = komut.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        ViewBag.donus = "eklemede_hata_oldu";

                    }
                    else
                    {
                        ViewBag.donus = "Ekleme Başarılı";
                        Response.Redirect("/home/login");

                    }


                    if (gr_con.State == ConnectionState.Open)
                        gr_con.Close();
                }
                catch
                {


                }

            }
            return View(model);

        }

        public ActionResult iletisim(login model)
        {
            return View();
        }

        public ActionResult Card(urunler model)
        {
            return PartialView(urun1_data(model));
        }
        public IEnumerable<urunler> urun1_data(urunler model)
        {
            using (var gr_con1 = new SqlConnection(WebConfigurationManager.ConnectionStrings["TSS"].ConnectionString))
            {
                if (gr_con1.State == ConnectionState.Closed)
                    gr_con1.Open();



                SqlCommand MyCommand22 = new SqlCommand("", gr_con1);
                MyCommand22.CommandText = "SELECT  * FROM Urunler where urun_id='" + model.Id + "'";

                SqlDataReader rd1 = MyCommand22.ExecuteReader();
                if (rd1.HasRows)
                {

                    while (rd1.Read())
                    {

                        yield return new urunler()
                        {

                            Id = (int)rd1["urun_id"],
                            Adi = (string)rd1["urun_adi"],
                            ozellık = (string)rd1["ozellık"],
                            Fiyat = (decimal)rd1["fiyat"],
                            beden = (string)rd1["beden"],
                            resim = (string)rd1["resim"],






                        };
                    }

                }



                if (gr_con1.State == ConnectionState.Open)
                    gr_con1.Close();
            }






        }

        public ActionResult product(Kategori model)
        {
            return PartialView(kategori_data(model));
           
        }

        public IEnumerable<Kategori> kategori_data(Kategori model)
        {

            using (var gr_con1 = new SqlConnection(WebConfigurationManager.ConnectionStrings["TSS"].ConnectionString))
            {
                if (gr_con1.State == ConnectionState.Closed)
                    gr_con1.Open();


                SqlCommand MyCommand22 = new SqlCommand("", gr_con1);
                MyCommand22.CommandText = "SELECT  * FROM Kategori";

                SqlDataReader rd1 = MyCommand22.ExecuteReader();
                if (rd1.HasRows)
                {

                    while (rd1.Read())
                    {

                        yield return new Kategori()
                        {
                            Id = (int)rd1["kategori_id"],
                            Adi = (string)rd1["kategori_adi"],









                        };
                    }

                }



                if (gr_con1.State == ConnectionState.Open)
                    gr_con1.Close();

            }
        }
        public ActionResult odeme(urunler model)
        {
            return PartialView(urun2_data(model));
        }
        public IEnumerable<urunler> urun2_data(urunler model)
        {
            using (var gr_con1 = new SqlConnection(WebConfigurationManager.ConnectionStrings["TSS"].ConnectionString))
            {
                if (gr_con1.State == ConnectionState.Closed)
                    gr_con1.Open();



                SqlCommand MyCommand22 = new SqlCommand("", gr_con1);
                MyCommand22.CommandText = "SELECT  * FROM Urunler where urun_id='" + model.Id + "'";

                SqlDataReader rd1 = MyCommand22.ExecuteReader();
                if (rd1.HasRows)
                {

                    while (rd1.Read())
                    {

                        yield return new urunler()
                        {

                            Id = (int)rd1["urun_id"],
                            Adi = (string)rd1["urun_adi"],
                            ozellık = (string)rd1["ozellık"],
                            Fiyat = (decimal)rd1["fiyat"],
                            beden = (string)rd1["beden"],
                            resim = (string)rd1["resim"],






                        };
                    }

                }



                if (gr_con1.State == ConnectionState.Open)
                    gr_con1.Close();
            }
        }
        public ActionResult siparis(siparis model)
        {
            try
            {
                SqlConnection gr_con = new SqlConnection(WebConfigurationManager.ConnectionStrings["TSS"].ConnectionString);
                if (gr_con.State == ConnectionState.Closed)
                    gr_con.Open();
                string kayit = "insert into Siparis (urun_id,adsoyad,adres,telefon) values (@alan1,@alan2,@alan3,@alan4)";
                SqlCommand komut = new SqlCommand(kayit, gr_con);
                if (ModelState.IsValid)
                {


                    komut.Parameters.AddWithValue("@alan1", model.id);
                    komut.Parameters.AddWithValue("@alan2", model.adsoyad);
                    komut.Parameters.AddWithValue("@alan3", model.adres);
                    komut.Parameters.AddWithValue("@alan4", model.telefon);










                }


                int rowsAffected = komut.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    ViewBag.donus = "eklemede_hata_oldu";

                }
                else
                {
                    ViewBag.donus = "Siparişiniz alınmıştır";
                    Response.Redirect("/home/tamam");

                }


                if (gr_con.State == ConnectionState.Open)
                    gr_con.Close();
            }
            catch
            {


            }
            return View(model);

        }
        public ActionResult tamam(siparis model)
        {
            Random rastgele = new Random();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 8; i++)
            {
                int ascii = rastgele.Next(65, 91);
                char karakter = Convert.ToChar(ascii);
                sb.Append(karakter);

            }
            ViewBag.donus = sb.ToString();
            return View();
        }
    }
}



        





        
