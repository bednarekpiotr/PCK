﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml;
using zad_5_wpf;
using System.Collections.ObjectModel;

namespace Ksiegarnia_zad_5
{

    public partial class MainWindow : Window
    {
        public Ksiegarnia Dane { get; set; } // tutaj znajdują się wszystkie dane do wyświetlania - trzeba jedynie dotrzeć do konkretnych property
        public XML Tools { get; set; }

        List<String> jezyki;
        List<String> waluty;

        ObservableCollection<String> ListaPozycji;

        public MainWindow()
        {
            InitializeComponent();


            Tools = new XML("..//..//Source//księgarnia_v1.3.xml", "..//..//Source//księgarnia_v1.3.xsd");

            Start();
            
        }

        private void fun()
        {
            foreach (var k in Dane.Ksiazki)
            {
                ListaPozycji.Add(k.Tytul.ToString());
            }
            foreach (var k in Dane.Czasopisma)
            {
                ListaPozycji.Add(k.Tytul.ToString());
            }
            foreach (var k in Dane.Ebooki)
            {
                ListaPozycji.Add(k.Tytul.ToString());
            }

        }

        private void Start()
        {
            if (!Tools.XmlFile.Exists)
            {
                MessageBox.Show("Błąd: Brak pliku: " + Tools.XmlFile.FullName.ToString(), "Wczytywanie danych");
            }
            else
            {
                Dane = Tools.Deserialize();
                this.MainDataContext.DataContext = Dane;
                this.MetadaneTxtBlock.DataContext = Dane.Metadane;

                ListaPozycji = new ObservableCollection<string>();
                waluty = new List<string>
            {
                "PLN",
                "EUR",
                "GBP",
                "USD"
            };
                cenacombo.ItemsSource = waluty;

                jezyki = new List<string>
            {
                "polski",
                "angielski",
                "niemiecki",
                "rosyjski"
            };
                jezykcombo.ItemsSource = jezyki;

                List<String> pozycja = new List<string>
            {
                "książka",
                "e-book",
                "czasopismo"
            };
                pozycjacombo.ItemsSource = pozycja;

                List<String> rozmiarilosc = new List<string>
            {
                "ilość stron:",
                "rozmiar w MB",
                "częstotliwość"
            };
                rozmiarilosccombo.ItemsSource = rozmiarilosc;

                fun();
                KsiegarniaListBox.ItemsSource = ListaPozycji;
            }

        }

        private void Wczytaj(object sender, RoutedEventArgs e)
        {
            if (KsiegarniaListBox.SelectedItem != null)
            {

                tytultext.Text = KsiegarniaListBox.SelectedItem.ToString();

                foreach (var item in Dane.Ksiazki)
                {
                    if (item.Tytul.Equals(KsiegarniaListBox.SelectedItem.ToString()))
                    {
                        isbntext.Text = item.ISBN.ToString();
                        rozmiarilosccombo.Text = "ilość stron:";
                        jezykcombo.Text = item.Jezyk.ToString();
                        wydanie.Text = item.Wydanie.NumerWydania.ToString();
                        wydaniemiejsce.Text = item.Wydanie.MiejsceWydania.ToString();
                        wydaniedata.Text = item.Wydanie.DataWydania.ToString("yyyy-MM-dd");
                        wydawnictwotext.Text = item.Wydawnictwo.ToString();                        
                        cenacombo.Text = item.Cena.Waluta.ToString();
                        cenatext.Text = item.Cena.Ile.ToString();

                        iloscRozmiartext.Text = item.IloscStron.ToString();
                        autorzytext.Text = item.Autorzy.ToString();
                        opisskroconytext.Text = item.Opis.OpisWstepny.ToString();
                        if (item.Opis.OpisPozostaly != null)
                            opistext.Text = item.Opis.OpisPozostaly.ToString();
                        else
                            opistext.Text = "";
                        pozycjacombo.Text = "książka";
                        idtextbox.Text = item.Id.ToString();
                    }
                }

                foreach (var item in Dane.Czasopisma)
                {
                    if (item.Tytul.Equals(KsiegarniaListBox.SelectedItem.ToString()))
                    {
                        isbntext.Text = item.ISBN.ToString();
                        rozmiarilosccombo.Text = "częstotliwość";
                        cenacombo.Text = item.Cena.Waluta.ToString();
                        jezykcombo.Text = item.Jezyk.ToString();
                        wydanie.Text = item.Wydanie.NumerWydania.ToString();
                        wydaniemiejsce.Text = item.Wydanie.MiejsceWydania.ToString();
                        wydaniedata.Text = item.Wydanie.DataWydania.ToString("yyyy-MM-dd");
                        wydawnictwotext.Text = item.Wydawnictwo.ToString();
                        
                        cenatext.Text = item.Cena.Ile.ToString();

                        iloscRozmiartext.Text = item.Czestotliwosc.ToString();
                        autorzytext.Text = item.Autorzy.ToString();
                        opisskroconytext.Text = item.Opis.OpisWstepny.ToString();
                        opistext.Text = item.Opis.OpisPozostaly.ToString();
                        pozycjacombo.Text = "czasopismo";
                        idtextbox.Text = item.Id.ToString();
                    }
                }
                foreach (var item in Dane.Ebooki)
                {
                    if (item.Tytul.Equals(KsiegarniaListBox.SelectedItem.ToString()))
                    {
                        isbntext.Text = item.ISBN.ToString();
                        rozmiarilosccombo.Text = "rozmiar w MB";
                       
                        cenacombo.Text = item.Cena.Waluta.ToString();
                        jezykcombo.Text = item.Jezyk.ToString();
                        wydanie.Text = item.Wydanie.NumerWydania.ToString();
                        wydaniemiejsce.Text = item.Wydanie.MiejsceWydania.ToString();
                        wydaniedata.Text = item.Wydanie.DataWydania.ToString("yyyy-MM-dd");
                        wydawnictwotext.Text = item.Wydawnictwo.ToString();
                        cenatext.Text = item.Cena.Ile.ToString();

                        iloscRozmiartext.Text = item.Rozmiar.ToString();
                        autorzytext.Text = item.Autorzy.ToString();
                        opisskroconytext.Text = item.Opis.OpisWstepny.ToString();
                        opistext.Text = item.Opis.OpisPozostaly.ToString();
                        pozycjacombo.Text = "e-book";
                        idtextbox.Text = item.Id.ToString();
                    }
                }
            }
            else
            {
                //MessageBox.Show("Wybierz pozycję z listy");
            }
        }
        private void Odswiez(object sender, RoutedEventArgs e)
        {
            Start();
        }

        private void Wyczysc(object sender, RoutedEventArgs e)
        {
            isbntext.Clear();
            tytultext.Clear();
            wydanie.Clear();
            wydawnictwotext.Clear();
            cenatext.Clear();
            autorzytext.Clear();
            opisskroconytext.Clear();
            iloscRozmiartext.Clear();
            wydanie.Clear();
            wydaniemiejsce.Clear();
            wydaniedata.Clear();
            idtextbox.Clear();
            
            opistext.Clear();
            //
            
        }

        private void Usun(object sender, RoutedEventArgs e)
        {
            XmlDocument xmldoc = new XmlDocument();
            FileStream fs = new FileStream("..//..//Source//księgarnia_v1.3.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            xmldoc.Load(fs);


            if (KsiegarniaListBox.SelectedItem != null)
            {
                XmlNodeList nodelistksiazka = xmldoc.GetElementsByTagName("książka");
                foreach (XmlNode item in nodelistksiazka)
                {
                    if (item["tytuł"].InnerText == KsiegarniaListBox.SelectedItem.ToString())
                    {
                        item.ParentNode.RemoveChild(item);
                        MessageBox.Show("Usunięto pozycję");
                        break;
                    }
                }

                XmlNodeList nodelistczasopismo = xmldoc.GetElementsByTagName("czasopismo");
                foreach (XmlNode item in nodelistczasopismo)
                {
                    if (item["tytuł"].InnerText == KsiegarniaListBox.SelectedItem.ToString())
                    {
                        item.ParentNode.RemoveChild(item);
                        MessageBox.Show("Usunięto pozycję");
                        break;
                    }
                }

                XmlNodeList nodelistebook = xmldoc.GetElementsByTagName("e-book");
                foreach (XmlNode item in nodelistebook)
                {
                    if (item["tytuł"].InnerText == KsiegarniaListBox.SelectedItem.ToString())
                    {

                        item.ParentNode.RemoveChild(item);
                        MessageBox.Show("Usunięto pozycję");
                        break;
                    }
                }
                fs.SetLength(0);
                xmldoc.Save(fs);
                fs.Close();

                Start();
            }
            else
            {
                MessageBox.Show("Wybierz pozycję");
            }

        }


        private void Button_Click_3(object sender, RoutedEventArgs e)

        {
            String namespaces = "http://www.example.org/typyNasze";
            FileStream fs = new FileStream("..//..//Source//księgarnia_v1.3.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            XmlDocument doc = new XmlDocument();
            doc.Load(fs);
            String pozycja = "książka";

            if (pozycjacombo.Text != null )
            {
               pozycja = pozycjacombo.Text.ToString();    
                //Tworzenie węzłów//

                XmlNode node = doc.CreateNode(XmlNodeType.Element, pozycja, namespaces);

                XmlNode pozycjaid = doc.CreateNode(XmlNodeType.Attribute, "pozycja_id","");
                XmlNode autorzyatribute = doc.CreateNode(XmlNodeType.Attribute, "autorzy", "");
                pozycjaid.Value = idtextbox.Text;
                autorzyatribute.Value = autorzytext.Text;
                node.Attributes.SetNamedItem(pozycjaid);
                node.Attributes.SetNamedItem(autorzyatribute);

                XmlNode tytul = doc.CreateElement("tytuł", namespaces);
                tytul.InnerText = tytultext.Text;
                node.AppendChild(tytul);

                XmlNode isbn = doc.CreateElement("ISBN", namespaces);
                isbn.InnerText = isbntext.Text;
                node.AppendChild(isbn);

                XmlNode jezyk = doc.CreateElement("język", namespaces);
                jezyk.InnerText = jezykcombo.Text;
                node.AppendChild(jezyk);

                XmlNode wydanie = doc.CreateElement("wydanie", namespaces);
                XmlNode miejscewydania = doc.CreateElement("miejsce_wydania", namespaces);
                miejscewydania.InnerText = wydaniemiejsce.Text;

                XmlNode datawydania = doc.CreateElement("data_wydania", namespaces);
                datawydania.InnerText = wydaniedata.Text;

                wydanie.AppendChild(miejscewydania);
                wydanie.AppendChild(datawydania);
                node.AppendChild(wydanie);

                XmlNode wydawnictwo = doc.CreateElement("wydawnictwo", namespaces);
                wydawnictwo.InnerText = wydawnictwotext.Text;
                node.AppendChild(wydawnictwo);

                XmlNode cena = doc.CreateElement("cena", namespaces);
                cena.InnerText = cenatext.Text.Replace(',','.');
                node.AppendChild(cena);

                XmlNode opis = doc.CreateElement("opis", namespaces);
                opis.InnerText = opistext.Text;
                
                XmlNode opisskrocony = doc.CreateElement("opis_skrócony", namespaces);
                opisskrocony.InnerText = opisskroconytext.Text;
                opis.AppendChild(opisskrocony);
                node.AppendChild(opis);

                doc.DocumentElement.AppendChild(node);

                fs.SetLength(0);
                doc.Save(fs);
                fs.Close();

            }
            else
            {
                MessageBox.Show("wybierz rodzaj pozycji");
            }
   
        }

        private void Zapisz(object sender, RoutedEventArgs e)
        {
            if (KsiegarniaListBox.SelectedItem != null)
            {
                             

                foreach (var item in Dane.Ksiazki)
                {
                    if (item.Tytul.Equals(KsiegarniaListBox.SelectedItem.ToString()))
                    {
                        item.Tytul = tytultext.Text;

                        item.ISBN = UInt64.Parse(isbntext.Text);
                        /*foreach(var j in jezyki)
                        { if (jezykcombo.Text == j)
                            { item.Jezyk = j; } }*/
                        item.Wydanie.NumerWydania = UInt16.Parse(wydanie.Text);
                        item.Wydanie.MiejsceWydania = wydaniemiejsce.Text;
                        item.Wydanie.DataWydania = DateTime.Parse(wydaniedata.Text);
                        item.Wydawnictwo = wydawnictwotext.Text;
                        /*foreach (var i in waluty)
                        {
                            if (cenacombo.Text == i)
                            { item.Cena.Waluta = i; }
                        }*/

                        item.Cena.Ile = float.Parse(cenatext.Text);

                        item.IloscStron = UInt16.Parse(iloscRozmiartext.Text);
                        item.Autorzy = autorzytext.Text;
                        item.Opis.OpisWstepny = opisskroconytext.Text;

                        if (item.Opis.OpisPozostaly != null)
                            item.Opis.OpisPozostaly = opistext.Text;
                        else
                            item.Opis.OpisPozostaly = "";

                        item.Id = idtextbox.Text;
                    }
                }

                foreach (var item in Dane.Czasopisma)
                {
                    if (item.Tytul.Equals(KsiegarniaListBox.SelectedItem.ToString()))
                    {
                        isbntext.Text = item.ISBN.ToString();
                        rozmiarilosccombo.Text = "częstotliwość";
                        cenacombo.Text = item.Cena.Waluta.ToString();
                        jezykcombo.Text = item.Jezyk.ToString();
                        wydanie.Text = item.Wydanie.NumerWydania.ToString();
                        wydaniemiejsce.Text = item.Wydanie.MiejsceWydania.ToString();
                        wydaniedata.Text = item.Wydanie.DataWydania.ToString("yyyy-MM-dd");
                        wydawnictwotext.Text = item.Wydawnictwo.ToString();

                        cenatext.Text = item.Cena.Ile.ToString();

                        iloscRozmiartext.Text = item.Czestotliwosc.ToString();
                        autorzytext.Text = item.Autorzy.ToString();
                        opisskroconytext.Text = item.Opis.OpisWstepny.ToString();
                        opistext.Text = item.Opis.OpisPozostaly.ToString();
                        pozycjacombo.Text = "czasopismo";
                        idtextbox.Text = item.Id.ToString();
                    }
                }
                foreach (var item in Dane.Ebooki)
                {
                    if (item.Tytul.Equals(KsiegarniaListBox.SelectedItem.ToString()))
                    {
                        isbntext.Text = item.ISBN.ToString();
                        rozmiarilosccombo.Text = "rozmiar w MB";

                        cenacombo.Text = item.Cena.Waluta.ToString();
                        jezykcombo.Text = item.Jezyk.ToString();
                        wydanie.Text = item.Wydanie.NumerWydania.ToString();
                        wydaniemiejsce.Text = item.Wydanie.MiejsceWydania.ToString();
                        wydaniedata.Text = item.Wydanie.DataWydania.ToString("yyyy-MM-dd");
                        wydawnictwotext.Text = item.Wydawnictwo.ToString();
                        cenatext.Text = item.Cena.Ile.ToString();

                        iloscRozmiartext.Text = item.Rozmiar.ToString();
                        autorzytext.Text = item.Autorzy.ToString();
                        opisskroconytext.Text = item.Opis.OpisWstepny.ToString();
                        opistext.Text = item.Opis.OpisPozostaly.ToString();
                        pozycjacombo.Text = "e-book";
                        idtextbox.Text = item.Id.ToString();
                    }
                }
            }
            Tools.SaveData(Dane);
            Start();
        }


    }
}

