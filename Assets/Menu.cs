using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	
	public int szerokoscOkna;
	public int wysokoscOkna;
	public int wysokoscOkna2;
	public int szerokoscOkna2;
	public string obecnePolozenie;
	public GUISkin skinMenu;
	public GUISkin skinjakosc;
	public GUISkin skinRozdzielczosc;
	public bool czyPokazac;
	public AudioSource efekty;
	public AudioSource muzyka;
	int obecnaJakosc;
	int ostatniaJakosc;
	int obecnyPelnyEkran;
	int ostatniPelnyekran;
	string[] listaEkran = {"Tak","Nie"};
	public bool pelenEkran = false;
	public Vector2 pozycjaScrolla = Vector2.zero;
	public FontStyle czcionka;
	
	
	
	// Use this for initialization
	void Start () {
//		poziomGlosnociEfektow = efekty.audio.volume;
		szerokoscOkna = 300;
		szerokoscOkna2 = szerokoscOkna - 10;
		wysokoscOkna2 = 60;
		obecnePolozenie = "Menu glowne";
		czyPokazac = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(obecnePolozenie == "Menu glowne")
		{
			wysokoscOkna = wysokoscOkna2 * 4 + 40;
		}
		if(obecnePolozenie == "Graj")
		{
			wysokoscOkna = wysokoscOkna2 * 3 + 40;
		}
		if(obecnePolozenie == "Wyjście")
		{
			wysokoscOkna = wysokoscOkna2 * 2 + 60;
		}
		if(obecnePolozenie == "Ustawienia")
		{
			wysokoscOkna = wysokoscOkna2 * 3 + 55;
		}

		if(obecnePolozenie == "Jakosc")
		{
			wysokoscOkna = wysokoscOkna2 * 3 + 55;
		}
		
	}
	void OnGUI()
	{
		if(obecnePolozenie == "Menu glowne" && czyPokazac == true)
		{
			GUI.skin = skinMenu;
			GUI.Box(new Rect(Screen.width / 2 - szerokoscOkna / 2, Screen.height / 2 - szerokoscOkna / 2, szerokoscOkna, wysokoscOkna),"");
			if(GUI.Button(new Rect(Screen.width / 2 - szerokoscOkna / 2 + 5, Screen.height / 2 - szerokoscOkna / 2 + 15, szerokoscOkna2, wysokoscOkna2),"Graj"))
			{	
				Application.LoadLevel(1);
			}
			if(GUI.Button(new Rect(Screen.width / 2 - szerokoscOkna / 2 + 5, Screen.height / 2 - szerokoscOkna / 2 + wysokoscOkna2 + 20, szerokoscOkna2, wysokoscOkna2),"Ustawienia"))
			{
				obecnePolozenie = "Ustawienia";
			}
			if(GUI.Button(new Rect(Screen.width / 2 - szerokoscOkna / 2 + 5, Screen.height / 2 - szerokoscOkna / 2 + wysokoscOkna2 * 2 + 25, szerokoscOkna2, wysokoscOkna2),"Twórcy"))
			{
				obecnePolozenie = "Twórcy";
			}
			if(GUI.Button(new Rect(Screen.width / 2 - szerokoscOkna / 2 + 5, Screen.height / 2 - szerokoscOkna / 2 + + wysokoscOkna2 * 3 + 30, szerokoscOkna2, wysokoscOkna2),"Wyjście"))
			{
				obecnePolozenie = "Wyjście";
			}
		}

		if(obecnePolozenie == "Wyjście")
		{
			GUI.skin = skinMenu;
			GUI.Box(new Rect(Screen.width / 2 - szerokoscOkna / 2, Screen.height / 2 - szerokoscOkna / 2, szerokoscOkna, wysokoscOkna),"");
			GUI.Box(new Rect(Screen.width / 2 - szerokoscOkna / 2, Screen.height / 2 - szerokoscOkna / 2, szerokoscOkna, 30),"Czy Na Pewno Chcesz Wyjść");
			if(GUI.Button(new Rect(Screen.width / 2 - szerokoscOkna / 2 + 5, Screen.height / 2 - szerokoscOkna / 2 + 40, szerokoscOkna2, wysokoscOkna2),"Tak"))
			{
				Application.Quit();
			}
			if(GUI.Button(new Rect(Screen.width / 2 - szerokoscOkna / 2 + 5, Screen.height / 2 - szerokoscOkna / 2 + wysokoscOkna2 + 45, szerokoscOkna2, wysokoscOkna2),"Nie"))
			{
				obecnePolozenie = "Menu glowne";
				czyPokazac = true;
			}
		}
		if(obecnePolozenie == "Ustawienia") {
	
			if(GUI.Button(new Rect(Screen.width / 2 - szerokoscOkna / 2 + 5, Screen.height / 2 - szerokoscOkna / 2 + wysokoscOkna2 * 2 + 45, szerokoscOkna2, wysokoscOkna2),"Powrót"))
			{
				obecnePolozenie = "Menu glowne";
			}
		}

		if(obecnePolozenie == "Twórcy")
		{
			GUI.skin = skinMenu;
			GUI.Box(new Rect(Screen.width / 2 - szerokoscOkna, Screen.height / 2 - wysokoscOkna, szerokoscOkna * 2, wysokoscOkna *1.5f), "");
			GUI.Box(new Rect(Screen.width / 2 - szerokoscOkna, Screen.height / 2 - wysokoscOkna, szerokoscOkna * 2, 30), "Twórcy");
			if(GUI.Button(new Rect(Screen.width / 2 - szerokoscOkna + 5, Screen.height / 2 - wysokoscOkna + 420,szerokoscOkna * 2 - 10, wysokoscOkna2),"Powrót"))
			{
				obecnePolozenie = "Menu glowne";
			}
			
			pozycjaScrolla = GUI.BeginScrollView(new Rect(Screen.width / 2 - szerokoscOkna + 10, Screen.height / 2 - wysokoscOkna + 40, szerokoscOkna * 2 - 10, wysokoscOkna2 * 6), pozycjaScrolla, new Rect(Screen.width / 2 - szerokoscOkna + 10, Screen.height / 2 - wysokoscOkna + 40, szerokoscOkna * 2 - 30, wysokoscOkna2 * 10));
			GUI.ScrollTowards(new Rect(Screen.width / 2 - szerokoscOkna + 10, Screen.height / 2 - wysokoscOkna + 40, szerokoscOkna * 2 - 30, wysokoscOkna2 * 200), 0.5f);
			GUI.Box(new Rect(Screen.width / 2 - szerokoscOkna + 10, Screen.height / 2 - wysokoscOkna +80, szerokoscOkna * 2 - 10, wysokoscOkna2),"Audi");
			GUI.Box(new Rect(Screen.width / 2 - szerokoscOkna + 10, Screen.height / 2 - wysokoscOkna + 80 +80, szerokoscOkna * 2 - 10, wysokoscOkna2),"The Light Team");
			GUI.Box(new Rect(Screen.width / 2 - szerokoscOkna + 10, Screen.height / 2 - wysokoscOkna + 180 +80, szerokoscOkna * 2 - 10, wysokoscOkna2),"Maciej Nowicki");
			GUI.Box(new Rect(Screen.width / 2 - szerokoscOkna + 10, Screen.height / 2 - wysokoscOkna + 250+80, szerokoscOkna * 2 - 10, wysokoscOkna2),"Krzysztof Kubiak");
			GUI.Box(new Rect(Screen.width / 2 - szerokoscOkna + 10, Screen.height / 2 - wysokoscOkna + 320+80, szerokoscOkna * 2 - 10, wysokoscOkna2),"Patryk Kubiak");
			GUI.Box(new Rect(Screen.width / 2 - szerokoscOkna + 10, Screen.height / 2 - wysokoscOkna + 390+80, szerokoscOkna * 2 - 10, wysokoscOkna2),"Dariusz");
			GUI.Box(new Rect(Screen.width / 2 - szerokoscOkna + 10, Screen.height / 2 - wysokoscOkna + 460+80, szerokoscOkna * 2 - 10, wysokoscOkna2),"Mateusz Janeczek");
			GUI.EndScrollView();
		}
		
	}
}
