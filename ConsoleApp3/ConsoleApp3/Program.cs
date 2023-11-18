/*
Kod se sastoji od:
 * početnih podataka
 * programa
 * pomočnih funkcija
 :( Svaki dio je odvojen linijom )/////)

Format komentiranja u programu: 
Naslov koda( što kod radi), kod izgleda ok(+), testirano i radi(+)
*/

var šifra = 0000;
//artikl{(ime, cijena, količina, datum isteka roka)}
var artikli = new List<Tuple<string, double, int, DateTime>>() {
	new("Luk",3,100,new DateTime(2024,1,1)),
	new("Jabuke",2,50,new DateTime(2023,12,11)),
	new("Jogurt",1.5,35,new DateTime(2021,12,11)),
	new("Kruške",5,70,new DateTime(2021,12,1)),
	new("Mlijeko",2.70,432,new DateTime(2024,10,23))
};
//radnici{(Ime, datumRođenja)}
var radnici = new List<Tuple<string, DateTime>>()
{
	{new ("Pavao Pavličić",new DateTime(1990,1,21))},
	{new ("aa",new DateTime(1990,12,3))},
	{new ("Ivan Ivanović",new DateTime(1983,11,13))},
	{new ("Hugo Cape",new DateTime(1911,1,21))},
	{new ("Marko Matić",new DateTime(1956,11,21))}
};
//računi{(id,datum izdavanja računa,{(artikli),(artikli)})}
var računi = new List<Tuple<int, DateTime, List<Tuple<string, double, int, DateTime>>>>()
{
	{new Tuple<int, DateTime, List<Tuple<string, double, int, DateTime>>>(0, DateTime.Now, new List<Tuple<string, double, int, DateTime>>{artikli[0]}) },
	{new Tuple<int, DateTime, List<Tuple<string, double, int, DateTime>>>(1, DateTime.Now, new List<Tuple<string, double, int, DateTime>>{artikli[0],artikli[2]}) },
	{new Tuple<int, DateTime, List<Tuple<string, double, int, DateTime>>>(2, DateTime.Now, new List<Tuple<string, double, int, DateTime>>{artikli[0]}) },
	{new Tuple<int, DateTime, List<Tuple<string, double, int, DateTime>>>(3, DateTime.Now, new List<Tuple<string, double, int, DateTime>>{artikli[0]}) }

};

///////////////////////////////////////////////////////////////////////////////////
// x služi za navigaciju kroz program
int x;
do
{	
	Console.Clear();
    x = OptionInput(new List<string> { "1 - Artikli", "2 - Radnici", "3 - Računi", "4 - Statistika"});
	
	//Artikli
	if (x == 1)
	{
		x = OptionInput(new List<string> { "1 - Unos artikla", "2 - Brisanje artikla", "3 - Uređivajne artikla", "4 - Ispis" });
		//Unos artikla + +
		if (x == 1)
		{
			var ime = StringInput(new List<string> { "Unesi ime artikla: " });
			var količina = IntInput(new List<String> { "Unesi količinu artikla: " });
			var cijena = DoubleInput(new List<String> { "Unesi cijenu artikla u eurima: " });
			var datumIstekaRoka = DateInput(new List<String> { "Unesi datum isteka roka u obliku yyyy-mm-dd: " });
			
			var tupl = new Tuple<string, double, int, DateTime>(ime, cijena, količina, datumIstekaRoka);
			artikli.Add(tupl);
		}

		//Brisanje artikla + +
		else if (x == 2)
		{
			x = OptionInput(new List<string> { "1 - Brisanje po imenu artikla: ", "2 - Izbriši sve kojima je istekao rok tranjana" });

			//Brisanje po imenu +
			if (x == 1)
			{
				var ime = StringInput(new List<String> { "Unesi ime artikla: " });
				bool nađen = false;
				foreach (var item in artikli)
				{
					if (item.Item1 == ime)
					{
						artikli.Remove(item);
						nađen = true;
						break;
					}
				}
				if (!nađen)
				{
					Wait("Artikl nije pronađen");
				}
			}
			//Brisanje po istečenom roku +
			else if (x == 2)
			{
				var privremenaLista = new List<Tuple<string, double, int, DateTime>>();
				foreach (var item in artikli)
				{
					if (DateTime.Now < item.Item4)
					{
						privremenaLista.Add(item);
					}
				}
				artikli = privremenaLista;
			}
		}

		//Uređivanje artikla + +
		else if (x == 3)
		{
			x = OptionInput(new List<string> { "1 - Uređivanje po imenu", "2 - Popust ili poskupljenje",  });
			
			//Uređivanje po imenu +
			if (x == 1)
			{
				var nađen = false;
				var ime = StringInput(new List<String> { "Unesi ime artikla: " });
				foreach (var item in artikli)
				{
					if (item.Item1 == ime)
					{
						nađen = true;
						x = IntInput(new List<String> { "1 - Ime", "2 - Cijena", "3 - Količina", "4 - Datum isteka roka" });
						//Uređivanje imena +
						if (x == 1)
						{
							var tupl = new Tuple<String, double, int, DateTime>(StringInput(new List<String> { "Unesi ime artikla: " }), item.Item2, item.Item3, item.Item4);
							artikli.Add(tupl);
							artikli.Remove(item);
							break;
						}
						//Uređivanje cijene +
						else if (x == 2)
						{
							var tupl = new Tuple<String, double, int, DateTime>(item.Item1, DoubleInput(new List<String> { "Unesi cijenu artikla: " }), item.Item3, item.Item4);
							artikli.Add(tupl);
							artikli.Remove(item);
							break;
						}
						//Uređivanje količine +
						else if (x == 3)
						{
							var tupl = new Tuple<String, double, int, DateTime>(item.Item1, item.Item2, IntInput(new List<String> { "Unesi količinu artikla: " }), item.Item4);
							artikli.Add(tupl);
							artikli.Remove(item);
							break;
						}
						//Uređivanje datuma isteka roka +
						else if (x == 4)
						{
							var tupl = new Tuple<String, double, int, DateTime>(item.Item1, item.Item2, item.Item3, DateInput(new List<String> { "Unesi datum isteka roka artikla: " }));
							artikli.Add(tupl);
							artikli.Remove(item);
							break;
						};
					}
				}
				if (!nađen)
				{
                    Wait("Artikl nije nađen");
                }
			}

			//Uređivanje cijene +
			else if (x == 2)
			{
				var koeficjent = DoubleInput(new List<string> { "Unesi broj s kojim će se sve cijene pomnožiti: " });
				var privremenaLista = new List<Tuple<String, double, int, DateTime>>();
				foreach (var item in artikli)
				{
					var tupl = new Tuple<String, double, int, DateTime>(item.Item1, item.Item2 * koeficjent, item.Item3, item.Item4);
					privremenaLista.Add(tupl);
				}
				artikli = privremenaLista;
			}
		}

		// Ispis artikala
		else if (x == 4)
		{
			x = OptionInput(new List<string> { "1 - Ispiši sve kako su spremljeni", "2 - Sortirano po imenu", "3 - Sortiranoo po datumu isteka roka uzlazno", "4 - Sortirano po  datumu isteka roka silazno", "5 - Sortirano po količini", "6 - Ispiši najprodavaniji artikl", "7 - Ispiši najmanje prodavan artikl" });
			//Ispiši bez promjena + +
			if (x == 1)
			{
				foreach (var item in artikli)
				{
					Console.WriteLine(item);
				}
			}
			//Ispiši po imenu + +
			else if (x == 2)
			{
				var privremenaLista = new List<String>();
				foreach (var item in artikli)
				{
					privremenaLista.Add(item.Item1);
				}
				privremenaLista.Sort();

				foreach (var item in privremenaLista)
				{
					foreach (var item2 in artikli) {
						if (item == item2.Item1)
						{
							Console.WriteLine(item2);
						}
					}
				}
			}
			//Ispiši po datumu uzlazno + +
			else if (x == 3)
			{
				var privremenaLista = new List<DateTime>();
				foreach (var item in artikli)
				{
					privremenaLista.Add(item.Item4);
				}
				privremenaLista.Sort();
				foreach (var item in privremenaLista)
				{
					foreach (var item2 in artikli)
					{
						if (item == item2.Item4)
						{
							Console.WriteLine(item2);
						}
					}
				}
			}
			//Ispiši po datumu silazno + +
			else if (x == 4)
			{
				var privremenaLista = new List<DateTime>();
				foreach (var item in artikli)
				{
					privremenaLista.Add(item.Item4);
				}
				privremenaLista.Sort();
				privremenaLista.Reverse();
				foreach (var item in privremenaLista)
				{
					foreach (var item2 in artikli)
					{
						if (item == item2.Item4)
						{
							Console.WriteLine(item2);
						}
					}
				}
			}
			//Ispiši po količini + +
			else if (x == 5)
			{
				var privremenaLista = new List<int>();
				foreach (var item in artikli)
				{
					privremenaLista.Add(item.Item3);
				}
				privremenaLista.Sort();
				privremenaLista.Reverse();
				foreach (var item in privremenaLista)
				{
					foreach (var item2 in artikli)
					{
						if (item == item2.Item3)
						{
							Console.WriteLine(item2);
						}
					}
				}
			}
			//Ispiši najprodavaniji + +
			else if (x == 6)
			{
				var količina = new List<Tuple<String, int>>();
				var sviProizvodi = new List<String>();
				foreach (var item in računi)
				{
					foreach (var item2 in item.Item3)
					{
						if (!( sviProizvodi.Contains(item2.Item1))){
							sviProizvodi.Add(item2.Item1);
						}
					}
				}
				foreach (var proizvod in sviProizvodi)
				{
					var kol = 0;
					foreach (var račun in računi)
					{
						foreach (var namirnica in račun.Item3)
						{
							if (!(proizvod == namirnica.Item1))
							{
								kol += namirnica.Item3;
							}
						}
					}
					var tupl = new Tuple<String, int>(proizvod, kol);
					količina.Add(tupl);
				}
				var privremenaLista = new List<int>();
				foreach (var item in količina)
				{
					privremenaLista.Add(item.Item2);
				}
				privremenaLista.Sort();

				foreach (var item in privremenaLista)
				{
					foreach (var item2 in količina)
					{
						if (item == item2.Item2)
						{
							Console.WriteLine(item2);
						}
					}
				}
			}
			//Ispiši najmanje prodavani + +
			else if (x == 7)
			{
				var količina = new List<Tuple<String, int>>();
				var sviProizvodi = new List<String>();
				foreach (var item in računi)
				{
					foreach (var item2 in item.Item3)
					{
						if (!(sviProizvodi.Contains(item2.Item1)))
						{
							sviProizvodi.Add(item2.Item1);
						}
					}
				}
				foreach (var proizvod in sviProizvodi)
				{
					var kol = 0;
					foreach (var račun in računi)
					{
						foreach (var namirnica in račun.Item3)
						{
							if (!(proizvod == namirnica.Item1))
							{
								kol += namirnica.Item3;
							}
						}
					}
					var tupl = new Tuple<String, int>(proizvod, kol);
					količina.Add(tupl);
				}
				//+
				var privremenaLista = new List<int>();
				foreach (var item in količina)
				{
					privremenaLista.Add(item.Item2);
				}
				privremenaLista.Sort();
				privremenaLista.Reverse();
				foreach (var item in privremenaLista)
				{
					foreach (var item2 in količina)
					{
						if (item == item2.Item2)
						{
							Console.WriteLine(item2);
						}
					}
				}
			}
			Wait("");
		}
	}

	//Radnici + +
	else if (x == 2)
	{
		x = OptionInput(new List<string> { "1 - Unos radnika", "2 - Brisanje radnika", "3 - Uređivanje radnika", "4 - Ispis radnika", "0 - Natrag" });
		
		//Unos radnika + +
		if (x == 1)
		{
			var ime = StringInput(new List<string> { "Unesi ime i prezime: " });
			var datumRođenja = DateInput(new List<string> { "Unesi datum rođenja u obliku yyyy-mm-dd " });

			var tupl = new Tuple<string, DateTime>(ime, datumRođenja);
			radnici.Add(tupl);
		}

		//Brisanje radnika + +
		else if (x == 2)
		{
			x = OptionInput(new List<string> { "1 - Izbriši radnika po imenu", "2 - Izbriši sve radnike starije od 65" });
			
			//Izbriši po imenu + +
			if (x == 1)
			{
				var ime = StringInput(new List<string> { "Unesi ime radnika: " });
				foreach (var item in radnici)
				{
					if (item.Item1 == ime)
					{
						radnici.Remove(item);
						break;
					}
				}
			}
			//Izbriši starije od 65 + +
			else if (x == 2)
			{
				var privremenaLista = new List<Tuple<string, DateTime>>();
				foreach (var item in radnici)
				{
					if (DateTime.Now - item.Item2 < new DateTime(2065, 1, 1) - new DateTime(2000, 1, 1))
					{
						privremenaLista.Add(item);
					}
				}
				radnici = privremenaLista;
			}
		}

		//Uređivanje radnika + +
		else if (x == 3)
		{
			x = OptionInput(new List<string> { "1 - Uredi ime radnika", "2 - Uredi datum rođenja radnika" });
			
			//Uredi ime radnika + +
			if (x == 1)
			{
				var ime = StringInput(new List<string> { "Unesi ime radnika kojeg želiš promijeniti: " });
				var novoIme = StringInput(new List<string> { "Unesi novo ime: " });
				foreach (var item in radnici)
				{
					if (item.Item1 == ime)
					{
						var tupl = new Tuple<String, DateTime>(novoIme, item.Item2);
						radnici.Remove(item);
						radnici.Add(tupl);
						break;
					}
				}
			}
			//Unsei novi datum rođenja + +
			else if (x == 2)
			{
				var ime = StringInput(new List<string> { "Unesi ime radnika kojeg želiš promijeniti: " });
				var noviDatumRođenja = DateInput(new List<string> { "Unesi novi datum rođenija u obliku yyyy-mm-dd: " });
				foreach (var item in radnici)
				{
					if (item.Item1 == ime)
					{
						var tupl = new Tuple<String, DateTime>(item.Item1, noviDatumRođenja);
						radnici.Remove(item);
						radnici.Add(tupl);
						break;
					}
				}
			}
		}

		//Ispis radnika + +
		else if (x == 4)
		{
			x = OptionInput(new List<string> { "1 - Ispiši sve radnike ", "2 - Ispiši sve radkike koji imaju rođendan u trenutnom mjesecu" });
			
			//Ispis svih radnika + +
			if (x == 1)
			{
				foreach (var item in radnici)
				{
					Console.WriteLine(item);
				}
			}
			//Ispis radnika sa rođendanom u trenutnom mjesecu + +
			else if (x == 2)
			{
				var sadašnjiMjesec = DateTime.Now.Month;
				foreach (var item in radnici)
				{
					if (item.Item2.Month == sadašnjiMjesec)
					{
						Console.WriteLine(item);
					}
				}
			}
			Wait("");

		}
	}

	//Računi + +
	else if (x == 3)
	{
		x = OptionInput(new List<string> { "1 - Unesi račun", "2 - Ispiši račun" });
		
		//Unos računa + +
		if (x == 1) { 
			//Unos namirnica + +
			var kupnjaGotova = false;
			var košarica = new List<Tuple<string, int>>();
			var artikliZaKupnju = new List<Tuple<string, double, int, DateTime>>(artikli);
			do
			{
				foreach (var item in artikliZaKupnju)
				{
					Console.WriteLine(item);
				};

				String ime;
				bool imeNaPopisu = false;
				do
				{
					ime = StringInput(new List<string> { "Unesi ime proizvoda: " });
					foreach (var item in artikliZaKupnju)
					{
						if (item.Item1 == ime)
						{
							imeNaPopisu = true;
							artikliZaKupnju.Remove(item);
							break;
						}
					}
				} while (!imeNaPopisu);

				var količina = IntInput(new List<string> { "Unesi količinu: " });
				košarica.Add(new Tuple<string, int>(ime, količina));
				x = OptionInput(new List<string> { "1 - Nastavi sa kupnjom", "2 - Nastavi na kasu" });
				kupnjaGotova = (x == 2);
			} while (!kupnjaGotova);
			
			//Brisanje košarice + +
			foreach (var item in košarica)
			{
				Console.WriteLine(item);
			};
			bool brisanjeKošarice = (OptionInput(new List<string> { "1 - Brisanje proizvoda iz košarice", "2 - Kupi" }) == 1);
			if (brisanjeKošarice) { 
				while (brisanjeKošarice)
				{
				
					String ime;
					bool imeNaPopisu = false;
					do
					{
						foreach (var item in košarica)
						{
							Console.WriteLine(item);
						};
						ime = StringInput(new List<string> { "Unesi ime proizvoda za brisanje: " });
						foreach (var item in košarica)
						{
							if (item.Item1 == ime)
							{
								imeNaPopisu = true;
								košarica.Remove(item);
								break;
							}
						}
					} while (!imeNaPopisu);
					brisanjeKošarice = (OptionInput(new List<string> { "1 - Nastavi sa brisanjem", "2 - Kupi" }) == 1);
				}
			}

			//Potvrda računa + +
			foreach (var item in košarica)
			{
				Console.WriteLine(item);
			};
			x = OptionInput( new List<string> { "1 - Potvrdi račun", "2 - Poništi račun"});
			if (x == 1)
			{
				var namirnice = new List<Tuple<string, double, int, DateTime>> ();
				foreach (var item in košarica)
				{
					foreach (var item2 in artikli)
					{
						if (item.Item1 == item2.Item1)
						{
							var tupl1 = new Tuple<string, double, int, DateTime>(item2.Item1, item2.Item2,item.Item2, item2.Item4);
							var tupl2 = new Tuple<string, double, int, DateTime> (item2.Item1, item2.Item2, item2.Item3 - item.Item2, item2.Item4);
							namirnice.Add(tupl1);
							artikli.Remove(item2);
							artikli.Add(tupl2);
							break;
						}
					}
				}

				var račun = new Tuple<int, DateTime, List<Tuple<string, double, int, DateTime>>>
					(računi.Last().Item1 + 1, DateTime.Now, namirnice);
				CWRačun(račun);
				Wait("");
				računi.Add(račun);
			}
			else
			{
				Wait("Račun poništen");
			}
		}

		//Ispis računa + +
		else if (x == 2)
		{
			x = OptionInput(new List<string> { "1 - Ispiši sve račune", "2 - Ispiši račun prema id" });
			
			//Ispiši sve račune + +
			if (x == 1)
			{
				foreach (var item in računi)
				{
					CWRačun(item);
				}    
			}
			//Ispiši račune prema id + +
			else if (x == 2)
			{
				int idRačuna ;
				do
				{
					idRačuna = IntInput(new List<string> { " Unesi id računa: " });
				} while (idRačuna > računi.Count()-1 || idRačuna < 0);
				foreach (var item in računi)
				{
                    if (item.Item1 == idRačuna)
					{
                        CWRačun(item);
						break;
                    }				
				}
            }
			Wait("");
		}
	}

	//Statistika + +
	else if (x == 4)
	{
		if (šifra == IntInput( new List<string> {"Unesi šifru: " }))
		{
			x = OptionInput(new List<string> { "1 - Ispiši opću statistiku", "2 - Izračunaj statistiku za određeni mjesec" });
			
			//Opća statistika + +
			if (x == 1) {
				var brojArtikala = 0;
				double VrijednostArtikala = 0;
				double VrijednostProdanihArtikala = 0;

				foreach (var item in artikli)
				{
					brojArtikala += item.Item3;
					VrijednostArtikala += item.Item2 * item.Item3;
				}

				foreach (var item in računi)
				{
					foreach (var item2 in item.Item3)
					{
						VrijednostProdanihArtikala += item2.Item2 * item2.Item3;
					}
				}

				Console.WriteLine("Broj artikala: " + brojArtikala);
				Console.WriteLine("Vrijednost artikala: " + VrijednostArtikala);
				Console.WriteLine("Vrijednost prodanih artikala: " + VrijednostProdanihArtikala);
				Wait("");
			}
			//Statistika za određeni mjesec + +
			else if (x == 2) {
				DateTime godina = new DateTime(IntInput(new List<string> { "Unesi godinu: " }), 1, 1);
				DateTime mjesec = new DateTime(2020, IntInput(new List<string> { "Unesi broj mjeseca: " }), 1);
				var troškovi = DoubleInput(new List<string> { "Unesi iznos plaće radnika: " }) + DoubleInput(new List<string> { "Unesi iznos najma: " }) + DoubleInput(new List<string> { "Unesi iznos ostalih troškova: " });
				double VrijednostProdanihArtikala = 0;

				foreach (var item in računi)
				{
					if (item.Item2.Month == mjesec.Month && item.Item2.Year == godina.Year) { 
						foreach (var item2 in item.Item3)
						{
							VrijednostProdanihArtikala += item2.Item2 * item2.Item3;
						}
					}
				}
				var zarada = VrijednostProdanihArtikala/3 - troškovi;
                Console.WriteLine(zarada);
                Wait("");
			}
		}
		else
		{
			Wait("Unesena je kriva šifra");
		}
	}
} while (x != 0);


//////////////////////////////////////////////////////////////////////////////////
//Pomoćne funkcije
static int IntInput(List<String> txt)
{
	foreach (var item in txt)
	{
		Console.WriteLine(item);
	}
	int y;
	bool uspješnoUneseno = false;
	do
	{
		uspješnoUneseno = int.TryParse(Console.ReadLine(), out y);
	} while (!uspješnoUneseno);
	Console.Clear();
    return y;
}
static int OptionInput(List<String> txt)
{
	Console.WriteLine("0 - Izlaz iz aplikacije");

	foreach (var item in txt)
	{
		Console.WriteLine(item);
	}
	int y = 0;
	bool uspješnoUneseno = false;
	do
	{
		uspješnoUneseno = int.TryParse(Console.ReadLine(), out y);
	} while (!uspješnoUneseno || y > txt.Count());
	Console.Clear();
	return y;
}
static String StringInput(List<String> txt)
{
	foreach (var item in txt)
	{
		Console.WriteLine(item);
	}
	var y = Console.ReadLine();
	Console.Clear();
	return y;
}
static DateTime DateInput(List<String> txt)
{
	foreach (var item in txt)
	{
		Console.WriteLine(item);
	}
	DateTime y;
	bool uspješnoUneseno = false;
	do
	{
		uspješnoUneseno = DateTime.TryParse(Console.ReadLine(), out y);
	} while (!uspješnoUneseno);
	Console.Clear();

	return y;
}
static double DoubleInput(List<String> txt)
{
	foreach (var item in txt)
	{
		Console.WriteLine(item);
	}
	double y;
	bool uspješnoUneseno = false;
	do
	{
		uspješnoUneseno = double.TryParse(Console.ReadLine(), out y);
	} while (!uspješnoUneseno);
	Console.Clear();

	return y;
}

static void Wait(String txt)
{
    Console.WriteLine(txt);
    Console.WriteLine("Klikni enter za nastavak: ");
    Console.ReadLine();
}
static void CWRačun(Tuple<int, DateTime, List<Tuple<string, double, int, DateTime>>> račun)
{
	double ukupnaCijena = 0;
	foreach (var item in račun.Item3)
	{
		ukupnaCijena += item.Item2 * item.Item3;
	}
    Console.WriteLine(račun.Item1 + " - " + račun.Item2 + " - " + ukupnaCijena + "e");
}