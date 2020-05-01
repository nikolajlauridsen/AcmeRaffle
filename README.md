# Acme Raffle

- [Acme Raffle](#acme-raffle)
  - [Getting started](#getting-started)
    - [Installing ASP.NET Core](#installing-aspnet-core)
    - [Launching the site](#launching-the-site)
      - [Using Visual Studio](#using-visual-studio)
      - [Using the commandline](#using-the-commandline)
  - [Taking the site for a spin](#taking-the-site-for-a-spin)

This is a sample project made for Umbraco as a part of their apprenticeship process.

The site is a landig page for a raffle held by a fictional company called "Acme Corporation".

The rules of the raffle is as follows

1. You need a valid serial number from a purchased product
2. You can enter twice for each valid serial number
3. You must be at least 18 years old to enter

## Getting started

### Installing ASP.NET Core

This project is made with ASP.NET Core, if you don't have ASP.NET Core installed you can install it easily with the Visual Studio installer. Simply open the Visual Studio installer and select modify on you Visual Studio version, now check the box for ASP.NET and Web development, see image below.

![Install asp](https://raw.githubusercontent.com/nikolajlauridsen/AcmeRaffle/master/ReadMeImages/install_asp.jpg)

You can always download and install ASP.NET Core if you don't wish to use Visual Studio, simply use this [link](https://dotnet.microsoft.com/download)

Once you have installed ASP.NET Core you should be ready to go. All required packages should be included in the project when you clone it, and the database is a sqlite database, which means there's no need for database setup, it's all ready to go.

### Launching the site

You can launch the site two different ways, through the commandline and with Visual Studio, with Visual Studio being the easiest way.

#### Using Visual Studio

Enter the AcmeRaffle folder and launch the soloution by double clicking the AcmeRaffle.sln file.

Once Visual Studio has loaded you might get a pop up asking you if you want to trust the self-signed SSL certificate, it's recommended, but not needed, to accept this prompt.

Now all you need to do is to ensure that AcmeRaffle is selected in the drop down menu, next to the play button, if that is the case simply push the play button, and the site should start, a web browser should open and navigate to the site as well.

![Run is vs](https://raw.githubusercontent.com/nikolajlauridsen/AcmeRaffle/master/ReadMeImages/run_vs.jpg)

#### Using the commandline

Open a commandline window and navigate to the AcmeRaffle folder within the cloned folder, and simply start the site with the following command:

```
dotnet run
```

The first time you run the command it might take a little while since it needs to build the project, simply wait a little and you'll see the following output.

![cli output](https://raw.githubusercontent.com/nikolajlauridsen/AcmeRaffle/master/ReadMeImages/run_cli.jpg)

To use the site open a browser and enter the url from the "Now listening on" output, marked in red in the screenshow.

## Taking the site for a spin

The database comes seeded with 100 valid serial numbers and 11 entries, this will allow you to have a look at the sites features without much hassle.

The valid serial numbers are listen in a newline seperated text file, in the root of the project, called "SerialNumbers.txt". The seeded entries are made using the serial numbers from the very bottom of the list, if you start from the top you should have plenty of serial numbers to have a peruse.

In order to see the raffle entries list you need to be logged in to the admin user, a default admin user is hardcoded into the project for this exact purpose, the credentials are:

Email: admin@user.com

Password: 1qaz"WSX

This should be everything you need to know to get started with the project and have a look around, enjoy