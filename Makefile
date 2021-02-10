main.exe: main.cs
	mcs main.cs -r:System.Web.Extensions.dll && mono main.exe "en.wikipedia.org"
