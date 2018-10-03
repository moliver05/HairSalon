# Hair Salon{C#}

### A Hair Salon App.

#### By **Mark Mangahas**

## Description
  An app for a hair salon. The owner should be able to add a list of the stylists, and for each stylist, add clients who see that stylist. The stylists work independently, so each client only belongs to a single stylist.


### Specs
| Spec | Input | Output |
| :-------------     | :------------- | :------------- |
| **Program Gathers Name of Stylists** | Stylist 1: "Mochi" | Stylist 2: "Teo" |
| **Program Gathers Name of Clients for each Stylists** | Client 1: "Bebot" | Client 2 : "Bebang"|
| **Program can Add New Client for each Specific Sylist** | New Client 2: "Bruno" | New Client 2 : "Bruna"|
| **Program can Delete the data of Stylist and its Clients** | 



## Setup/Installation Requirements

1. Clone this repository at https://github.com/moliver05/Salon
2. Go to folder directory HairSalon.Solution/HairSalon
3. Open MAMP and start servers
4. Go to MySQL from terminal
5. > CREATE DATABASE hair_salon;
6. > USE hair_salon;
7. > CREATE TABLE stylists (id serial PRIMARY KEY, name VARCHAR(255));
8. > CREATE TABLE clients (id serial PRIMARY KEY, name VARCHAR(255));
9. Run through local host by typing "dotnet run"
10. Open Web Browser (Chrome, Mozilla, Safari, etc.)
11. Open http://localhost:5000 in web browser.

## Known Bugs
* No known bugs at this time.

## Technologies Used
* C-Sharp
* MS Test
* Netcoreapp 1.1
* Atom
* GitHub
* MS MVC
* MySQL
* myPhpAdmin


## Support and contact details

_mo.mangahas@gmail.com_

### License

*{This software is licensed under the MIT license}*

Copyright (c) 2018 **_{Mark Mangahas}_**
