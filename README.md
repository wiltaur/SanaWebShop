# SanaWebShop
Test for Sana Company.

This is a Web Shop developed in React JS and Net Core Restfull API developed for testing at Sana.

The DB that is used is local with Sql Server "**SQLEXPRESS**" and we worked with the user sa, giving it an initial password with which it connected to the ORM Entity Framework. The Database scripts are in the folder: **"ScriptsDB"**.

This repository has one image that show the MER.

To keep in mind:
- In Front, first create a Customer for enable Shopping Cart.
- The method that lists the products was developed thinking of a view with a table, the indicated columns and also with a pagination.
- Some unit tests were developed with XUnit to the Controller, Business and the Repository.
- The front use Sesion Storage for save temporally information.
