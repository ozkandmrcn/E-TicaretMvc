using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using ticaret.App_Classes;
using ticaret.Models;





namespace ticaret.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Urunler(urunler model)
        {

            return PartialView(urunler_data(model));
        }





        public IEnumerable<urunler> urunler_data(urunler model)
        {

            using (var gr_con = new SqlConnection(WebConfigurationManager.ConnectionStrings["TSS"].ConnectionString))
            {
                if (gr_con.State == ConnectionState.Closed)
                    gr_con.Open();


                SqlCommand MyCommand22 = new SqlCommand("", gr_con);
                MyCommand22.CommandText = "SELECT  * FROM Urunler";

                SqlDataReader rd3 = MyCommand22.ExecuteReader();
                if (rd3.HasRows)
                {

                    while (rd3.Read())
                    {

                        yield return new urunler()
                        {
                            Id = (int)rd3["urun_id"],
                            Adi = (string)rd3["urun_adi"],
                            ozellık = (string)rd3["ozellık"],
                            Fiyat = (decimal)rd3["fiyat"],
                            beden = (string)rd3["beden"],
                            //  EklenmeTarihi = (DateTime)rd3["eklenmetarih"],


                            //KategoriID = (int)rd3["kategori_id"],
                            // MarkaID = (int)rd3["marka_id"],

                        };
                    }

                }

                if (gr_con.State == ConnectionState.Open)
                    gr_con.Close();
            }
        }

        public ActionResult UrunEkle()
        {

            return View();
        }
        [HttpPost]
        public ActionResult UrunEkle(urunler model, HttpPostedFileBase file)
        {



            if (file != null && file.ContentLength > 0)
            {
                //  var path = Path.Combine(Server.MapPath("~/Content/UrunResim"), file.FileName);
                //  file.SaveAs(path);
                // TempData["result"] = "Güncelleme Başarılı.";
                var resimAdi = Path.GetFileName(file.FileName);//Resmin neme ini al.

                var resimYolu = Path.Combine(Server.MapPath("~/Content/UrunResim"), resimAdi);
                file.SaveAs(resimYolu);


                SqlConnection gr_con = new SqlConnection(WebConfigurationManager.ConnectionStrings["TSS"].ConnectionString);
                {
                    if (gr_con.State == ConnectionState.Closed)
                        gr_con.Open();

                    string kayit = "insert into Urunler(urun_adi,fiyat,beden,ozellık,resim) values (@urun_adi,@fiyat,@beden,@ozellik,@resim)";
                    SqlCommand komut = new SqlCommand(kayit, gr_con);


                    komut.Parameters.AddWithValue("@urun_adi", model.Adi);
                    komut.Parameters.AddWithValue("@fiyat", model.Fiyat);
                    komut.Parameters.AddWithValue("@beden", model.beden);
                    komut.Parameters.AddWithValue("@ozellik", model.ozellık);
                    komut.Parameters.AddWithValue("@resim", resimYolu);
                    //  komut.Parameters.AddWithValue("@tarih",DateTime.Now);
                    // komut.Parameters.Add("@resim", SqlDbType.Image,resimYolu.Length).Value=resimYolu;




                    int rowsAffected = komut.ExecuteNonQuery();


                    if (rowsAffected == 0)
                    {
                        ViewBag.donus = "eklemede_hata_oldu";

                    }
                    else
                    {
                        ViewBag.donus = "Ekleme Başarılı";
                        Response.Redirect("/Admin/UrunEkle");

                    }

                    if (gr_con.State == ConnectionState.Open)
                        gr_con.Close();


                }

            }




            return View();



        }

        public ActionResult UrunSil(urunler model)
        {


            using (var gr_con = new SqlConnection(WebConfigurationManager.ConnectionStrings["TSS"].ConnectionString))
            {
                if (gr_con.State == ConnectionState.Closed)
                    gr_con.Open();
                string sorgu = "delete Urunler WHERE urun_id =@id";
                SqlCommand sqlkomut = new SqlCommand(sorgu, gr_con);
                sqlkomut.Parameters.Add("@id", SqlDbType.Int).Value = model.Id;



                sqlkomut.ExecuteNonQuery();


                ViewBag.donus = "Silme Başarılı";
                Response.Redirect("/Admin/Urunler");


                if (gr_con.State == ConnectionState.Open)
                    gr_con.Close();


            }
            return View(model);
        }


        public ActionResult Markalar(Marka model)
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









                        };
                    }

                }



                if (gr_con1.State == ConnectionState.Open)
                    gr_con1.Close();

            }

        }

        public ActionResult MarkaEkle()
        {

            return View();
        }
        [HttpPost]

        public ActionResult MarkaEkle(Marka model)
        {
            SqlConnection gr_con1 = new SqlConnection(WebConfigurationManager.ConnectionStrings["TSS"].ConnectionString);
            if (gr_con1.State == ConnectionState.Closed)
                gr_con1.Open();
            string kayit1 = "insert into Marka (marka_adi,aciklama) values (@k_adi,@yazar)";
            SqlCommand komut1 = new SqlCommand(kayit1, gr_con1);
            komut1.Parameters.AddWithValue("@k_adi", model.Adi);
            komut1.Parameters.AddWithValue("@yazar", model.Aciklama);


            int rowsAffected = komut1.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                ViewBag.donus = "eklemede_hata_oldu";

            }
            else
            {
                ViewBag.donus = "Ekleme Başarılı";
                Response.Redirect("/Admin/MarkaEkle");


            }

            if (gr_con1.State == ConnectionState.Open)
                gr_con1.Close();

            return View();
        }

        public ActionResult MarkaSil(string id, Marka model)
        {



            using (var gr_con = new SqlConnection(WebConfigurationManager.ConnectionStrings["TSS"].ConnectionString))
            {
                if (gr_con.State == ConnectionState.Closed)
                    gr_con.Open();
                string sorgu = "delete Marka WHERE marka_id =@id";
                SqlCommand sqlkomut = new SqlCommand(sorgu, gr_con);
                sqlkomut.Parameters.Add("@id", SqlDbType.Int).Value = model.Id;



                sqlkomut.ExecuteNonQuery();


                ViewBag.donus = "Silme Başarılı";
                Response.Redirect("/Admin/Markalar");


                if (gr_con.State == ConnectionState.Open)
                    gr_con.Close();


            }
            return View(model);
        }


        public ActionResult Kategoriler(Kategori model)
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

        public ActionResult KategoriEkle()
        {
            /*using (var gr_con = new SqlConnection(WebConfigurationManager.ConnectionStrings["TSS"].ConnectionString))
            {
                if (gr_con.State == ConnectionState.Closed)
                    gr_con.Open();
                SqlCommand MyCommand22 = new SqlCommand("", gr_con);
                MyCommand22.CommandText = "SELECT top 1 *  FROM Kategori where kategori_id = '" + id + "'";
                SqlDataReader rdr2 = MyCommand22.ExecuteReader();
                if (rdr2.HasRows)
                {
                    while (rdr2.Read())
                    {
                        model.Id = (int)rdr2["kategori_id"];
                        model.Adi = (string)rdr2["kategori_adi"];


                    }
                }
            }*/
            return View();

        }

        [HttpPost]
        public ActionResult KategoriEkle(Kategori model)
        {

            SqlConnection gr_con1 = new SqlConnection(WebConfigurationManager.ConnectionStrings["TSS"].ConnectionString);
            if (gr_con1.State == ConnectionState.Closed)
                gr_con1.Open();
            string kayit1 = "insert into Kategori (kategori_adi) values (@k_adi)";
            SqlCommand komut1 = new SqlCommand(kayit1, gr_con1);
            komut1.Parameters.AddWithValue("@k_adi", model.Adi);



            int rowsAffected = komut1.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                ViewBag.donus = "eklemede_hata_oldu";

            }
            else
            {
                ViewBag.donus = "Ekleme Başarılı";
                Response.Redirect("/Admin/KategoriEkle");


            }

            if (gr_con1.State == ConnectionState.Open)
                gr_con1.Close();

            return View();

        }



        public ActionResult KategoriSil(int id, Kategori model)
        {


            using (var gr_con = new SqlConnection(WebConfigurationManager.ConnectionStrings["TSS"].ConnectionString))
            {
                if (gr_con.State == ConnectionState.Closed)
                    gr_con.Open();
                string sorgu = "delete Kategori WHERE kategori_id =@id";
                SqlCommand sqlkomut = new SqlCommand(sorgu, gr_con);
                sqlkomut.Parameters.Add("@id", SqlDbType.Int).Value = model.Id;



                sqlkomut.ExecuteNonQuery();


                ViewBag.donus = "Silme Başarılı";
                Response.Redirect("/Admin/Kategoriler");


                if (gr_con.State == ConnectionState.Open)
                    gr_con.Close();


            }
            return View();
        }


        public ActionResult KategoriGuncelle(Kategori model, string id)
        {


            using (var gr_con = new SqlConnection(WebConfigurationManager.ConnectionStrings["TSS"].ConnectionString))
            {
                if (gr_con.State == ConnectionState.Closed)
                    gr_con.Open();
                SqlCommand MyCommand22 = new SqlCommand("", gr_con);
                MyCommand22.CommandText = "SELECT top 1 *  FROM Kategori where kategori_id = '" + id + "'";
                SqlDataReader rdr2 = MyCommand22.ExecuteReader();
                if (rdr2.HasRows)
                {
                    while (rdr2.Read())
                    {
                        model.Id = (int)rdr2["kategori_id"];
                        model.Adi = (string)rdr2["kategori_adi"];
                        // model.soyad = (string)rdr2["alan2"];
                    }
                }
            }


            return View(model);

        }



        [HttpPost]
        public ActionResult KategoriGuncelle(Kategori model)
        {
            using (var gr_con = new SqlConnection(WebConfigurationManager.ConnectionStrings["TSS"].ConnectionString))

            {



                if (gr_con.State == ConnectionState.Closed)
                    gr_con.Open();
                string sorgu = "UPDATE Kategori  SET kategori_adi=@alan1 WHERE kategori_id=@id";
                SqlCommand sqlkomut = new SqlCommand(sorgu, gr_con);

                sqlkomut.Parameters.Add("@id", SqlDbType.Int).Value = model.Id;
                sqlkomut.Parameters.Add("@alan1", SqlDbType.NChar).Value = model.Adi;




                sqlkomut.ExecuteNonQuery();
                ViewBag.donus = "Guncelleme Başarılı";
                // Response.Redirect("/Admin/KategoriEkle");


                if (gr_con.State == ConnectionState.Open)
                    gr_con.Close();





            }
            return View(model);
        }
        public ActionResult SliderResimleri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SliderResimleri(HttpPostedFileBase file)
        {

            if (file != null && file.ContentLength > 0)
            {
                //  var path = Path.Combine(Server.MapPath("~/Content/UrunResim"), file.FileName);
                //  file.SaveAs(path);
                // TempData["result"] = "Güncelleme Başarılı.";
                var resimAdi = Path.GetFileName(file.FileName);//Resmin neme ini al.

                var resimYolu = Path.Combine(Server.MapPath("~/Content/SliderResim"), resimAdi);
                file.SaveAs(resimYolu);

                SqlConnection gr_con1 = new SqlConnection(WebConfigurationManager.ConnectionStrings["TSS"].ConnectionString);
                {
                    if (gr_con1.State == ConnectionState.Closed)
                        gr_con1.Open();
                    string kayit1 = "insert into Resim(BuyukYol) values (@k_adi)";
                    SqlCommand komut1 = new SqlCommand(kayit1, gr_con1);
                    komut1.Parameters.AddWithValue("@k_adi", resimYolu);



                    int rowsAffected = komut1.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        ViewBag.donus = "eklemede_hata_oldu";

                    }
                    else
                    {
                        ViewBag.donus = "Ekleme Başarılı";
                        Response.Redirect("/Admin/SliderResimleri");


                    }

                    if (gr_con1.State == ConnectionState.Open)
                        gr_con1.Close();
                }


            }
            return View();

        }
        public ActionResult MarkaGuncelle(Marka model, string id)
        {


            using (var gr_con = new SqlConnection(WebConfigurationManager.ConnectionStrings["TSS"].ConnectionString))
            {
                if (gr_con.State == ConnectionState.Closed)
                    gr_con.Open();
                SqlCommand MyCommand22 = new SqlCommand("", gr_con);
                MyCommand22.CommandText = "SELECT top 1 *  FROM Marka where marka_id = '" + id + "'";
                SqlDataReader rdr2 = MyCommand22.ExecuteReader();
                if (rdr2.HasRows)
                {
                    while (rdr2.Read())
                    {
                        model.Id = (int)rdr2["marka_id"];
                        model.Adi = (string)rdr2["marka_adi"];

                    }
                }
            }


            return View(model);

        }



        [HttpPost]
        public ActionResult MarkaGuncelle(Marka model)
        {
            using (var gr_con = new SqlConnection(WebConfigurationManager.ConnectionStrings["TSS"].ConnectionString))

            {



                if (gr_con.State == ConnectionState.Closed)
                    gr_con.Open();
                string sorgu = "UPDATE Marka  SET marka_adi=@alan1 WHERE marka_id=@id";
                SqlCommand sqlkomut = new SqlCommand(sorgu, gr_con);

                sqlkomut.Parameters.Add("@id", SqlDbType.Int).Value = model.Id;
                sqlkomut.Parameters.Add("@alan1", SqlDbType.NChar).Value = model.Adi;




                sqlkomut.ExecuteNonQuery();
                ViewBag.donus = "Guncelleme Başarılı";



                if (gr_con.State == ConnectionState.Open)
                    gr_con.Close();





            }
            return View(model);
        }
        public ActionResult UrunGuncelle(urunler model, string id)
        {


            using (var gr_con = new SqlConnection(WebConfigurationManager.ConnectionStrings["TSS"].ConnectionString))
            {
                if (gr_con.State == ConnectionState.Closed)
                    gr_con.Open();
                SqlCommand MyCommand22 = new SqlCommand("", gr_con);
                MyCommand22.CommandText = "SELECT top 1 *  FROM Urunler where urun_id = '" + id + "'";
                SqlDataReader rdr2 = MyCommand22.ExecuteReader();
                if (rdr2.HasRows)
                {
                    while (rdr2.Read())
                    {
                        model.Id = (int)rdr2["urun_id"];
                        model.Adi = (string)rdr2["urun_adi"];
                        model.ozellık = (string)rdr2["ozellık"];
                        model.Fiyat = (decimal)rdr2["fiyat"];
                        model.beden = (string)rdr2["beden"];

                    }
                }
            }


            return View(model);

        }



        [HttpPost]
        public ActionResult UrunGuncelle(urunler model)
        {
            using (var gr_con = new SqlConnection(WebConfigurationManager.ConnectionStrings["TSS"].ConnectionString))

            {



                if (gr_con.State == ConnectionState.Closed)
                    gr_con.Open();
                string sorgu = "UPDATE Urunler  SET urun_adi=@alan1,fiyat=@alan2,beden=@alan3,ozellık=@alan4 WHERE urun_id=@id";
                SqlCommand sqlkomut = new SqlCommand(sorgu, gr_con);

                sqlkomut.Parameters.Add("@id", SqlDbType.Int).Value = model.Id;
                sqlkomut.Parameters.Add("@alan1", SqlDbType.NChar).Value = model.Adi;
                sqlkomut.Parameters.Add("@alan2", SqlDbType.Decimal).Value = model.Fiyat;
                sqlkomut.Parameters.Add("@alan3", SqlDbType.NChar).Value = model.beden;
                sqlkomut.Parameters.Add("@alan4", SqlDbType.NChar).Value = model.ozellık;




                sqlkomut.ExecuteNonQuery();
                ViewBag.donus = "Guncelleme Başarılı";



                if (gr_con.State == ConnectionState.Open)
                    gr_con.Close();





            }
            return View(model);
        }

        public ActionResult Kullanicilar(login model)
        {

            return PartialView(urunler_data(model));
        }





        public IEnumerable<login> urunler_data(login model)
        {

            using (var gr_con = new SqlConnection(WebConfigurationManager.ConnectionStrings["TSS"].ConnectionString))
            {
                if (gr_con.State == ConnectionState.Closed)
                    gr_con.Open();


                SqlCommand MyCommand22 = new SqlCommand("", gr_con);
                MyCommand22.CommandText = "SELECT  * FROM Kullanıcı";

                SqlDataReader rd3 = MyCommand22.ExecuteReader();
                if (rd3.HasRows)
                {

                    while (rd3.Read())
                    {

                        yield return new login()
                        {
                            id = (int)rd3["kullanici_id"],
                            isim = (string)rd3["isim"],
                            soyisim = (string)rd3["soyisim"],
                            tc = (string)rd3["tc"],
                            email = (string)rd3["e_posta"],
                            tel = (string)rd3["tel"],
                            dogrulama = (string)rd3["onaykodu"],
                            kullanici_adi = (string)rd3["kullanici_adi"],
                            sifre = (string)rd3["sifre"],
                            yetki = (int)rd3["yetki_id"],

                            //  EklenmeTarihi = (DateTime)rd3["eklenmetarih"],


                            //KategoriID = (int)rd3["kategori_id"],
                            // MarkaID = (int)rd3["marka_id"],

                        };
                    }

                } if (gr_con.State == ConnectionState.Open)
                    gr_con.Close();

            }
               
        }
        public ActionResult KullaniciEkle()
        {

            return View();
        }
        [HttpPost]

        public ActionResult KullaniciEkle(login model)
        {
            SqlConnection gr_con1 = new SqlConnection(WebConfigurationManager.ConnectionStrings["TSS"].ConnectionString);
            if (gr_con1.State == ConnectionState.Closed)
                gr_con1.Open();
            string kayit1 = "insert into Kullanıcı (isim,soyisim,tc,e_posta,tel,onaykodu,kullanici_adi,sifre,yetki_id) values (@alan1,@alan2,@alan3,@alan4,@alan5,@onay,@alan6,@alan7,@alan8)";
            SqlCommand komut1 = new SqlCommand(kayit1, gr_con1);
            komut1.Parameters.AddWithValue("@alan1", model.isim);
            komut1.Parameters.AddWithValue("@alan2", model.soyisim);
            komut1.Parameters.AddWithValue("@alan3", model.tc);
            komut1.Parameters.AddWithValue("@alan4", model.email);
            komut1.Parameters.AddWithValue("@alan5", model.tel);
            komut1.Parameters.AddWithValue("@onay", model.dogrulama);
            komut1.Parameters.AddWithValue("@alan6", model.kullanici_adi);
            komut1.Parameters.AddWithValue("@alan7", model.sifre);
            komut1.Parameters.AddWithValue("@alan8", model.yetki);


            int rowsAffected = komut1.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                ViewBag.donus = "eklemede_hata_oldu";

            }
            else
            {
                ViewBag.donus = "Ekleme Başarılı";
                Response.Redirect("/Admin/KullaniciEkle");


            }

            if (gr_con1.State == ConnectionState.Open)
                gr_con1.Close();

            return View();
        }
        public ActionResult  KullaniciSil(int id, login model)
        {


            using (var gr_con = new SqlConnection(WebConfigurationManager.ConnectionStrings["TSS"].ConnectionString))
            {
                if (gr_con.State == ConnectionState.Closed)
                    gr_con.Open();
                string sorgu = "delete Kullanıcı WHERE kullanici_id =@id";
                SqlCommand sqlkomut = new SqlCommand(sorgu, gr_con);
                sqlkomut.Parameters.Add("@id", SqlDbType.Int).Value = model.id;



                sqlkomut.ExecuteNonQuery();


                ViewBag.donus = "Silme Başarılı";
                Response.Redirect("/Admin/Kullanicilar");


                if (gr_con.State == ConnectionState.Open)
                    gr_con.Close();


            }
            return View();
        }

        public ActionResult KullaniciGuncelle(login model,string id)
        {


            using (var gr_con = new SqlConnection(WebConfigurationManager.ConnectionStrings["TSS"].ConnectionString))
            {
                if (gr_con.State == ConnectionState.Closed)
                    gr_con.Open();
                SqlCommand MyCommand22 = new SqlCommand("", gr_con);
                MyCommand22.CommandText = "SELECT top 1 *  FROM Kullanıcı where kullanici_id = '" + id + "'";
                SqlDataReader rd3 = MyCommand22.ExecuteReader();
                if (rd3.HasRows)
                {
                    while (rd3.Read())
                    {




                        model.id =(int)rd3["kullanici_id"];
                        model.isim = (string)rd3["isim"];
                        model.soyisim = (string)rd3["soyisim"];
                        model.tc = (string)rd3["tc"];
                        model.email = (string)rd3["e_posta"];
                        model.tel = (string)rd3["tel"];
                        model.dogrulama = (string)rd3["onaykodu"];
                        model.kullanici_adi =(string)rd3["kullanici_adi"];
                        model.sifre = (string)rd3["sifre"];
                        model.yetki = (int)rd3["yetki_id"];

                    }
                }
            }


            return View(model);

        }



        [HttpPost]
        public ActionResult KullaniciGuncelle(login model)
        {
            using (var gr_con = new SqlConnection(WebConfigurationManager.ConnectionStrings["TSS"].ConnectionString))

            {



                if (gr_con.State == ConnectionState.Closed)
                    gr_con.Open();
                string sorgu = "UPDATE Kullanıcı  SET isim=@alan1,soyisim=@alan2,tc=@alan3,e_posta=@alan4,tel=@alan5,onaykodu=@alan6,kullanici_adi=@alan7,sifre=@alan8,yetki_id=@alan9 WHERE kullanici_id=@id";
                SqlCommand sqlkomut = new SqlCommand(sorgu, gr_con);

                sqlkomut.Parameters.Add("@id", SqlDbType.Int).Value = model.id;
                sqlkomut.Parameters.Add("@alan1", SqlDbType.NChar).Value = model.isim;
                sqlkomut.Parameters.Add("@alan2", SqlDbType.NChar).Value = model.soyisim;
                sqlkomut.Parameters.Add("@alan3", SqlDbType.NChar).Value = model.tc;
                sqlkomut.Parameters.Add("@alan4", SqlDbType.NChar).Value = model.email;
                sqlkomut.Parameters.Add("@alan5", SqlDbType.NChar).Value = model.tel;
                sqlkomut.Parameters.Add("@alan6", SqlDbType.NChar).Value = model.dogrulama;
                sqlkomut.Parameters.Add("@alan7", SqlDbType.NChar).Value = model.kullanici_adi;
                sqlkomut.Parameters.Add("@alan8", SqlDbType.NChar).Value = model.sifre;
                sqlkomut.Parameters.Add("@alan9", SqlDbType.Int).Value = model.yetki;




                sqlkomut.ExecuteNonQuery();
                ViewBag.donus = "Guncelleme Başarılı";



                if (gr_con.State == ConnectionState.Open)
                    gr_con.Close();





            }
            return View(model);

        }
        }
}

    


    