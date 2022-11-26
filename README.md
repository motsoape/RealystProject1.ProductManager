# **ProductManager**

## Overview
This project consists of both .Net MVC, .Net API and .Net Console Application. 

### ProductManager MVC and API
This project is located under ProductManager.
- Update the connection string on appsettings.json for your sql database connection.
```json
"ConnectionString": {
    "ProductManagerDB": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ProductManagerDb;Integrated Security=True"
  }
```
Once the database connection string is set, you can run the application, this will create the database if it does not exists and seed the Product table with two records and seed Comment table with only one comment for the first product.

- You can access the swagger documentation for Product Manager Rest API on http://localhost:5089/swagger/index.html 
![image](https://user-images.githubusercontent.com/10927151/204085708-404a9d6f-e941-4663-a51c-bbcb85f6bb79.png)

The web application can be acccessed throught http://localhost:5089

- Once the application has been opened then you will be presented with a default page which is product page.
![image](https://user-images.githubusercontent.com/10927151/204085793-1e7a6e9d-386d-4db3-b119-4f8dc92abfeb.png)
You can filter the table with the search input field at your right, and you can also sort the table using table calumn sort buttons.
 
- Click "Add Product" to add the product
![image](https://user-images.githubusercontent.com/10927151/204085901-e6146b4f-1311-4911-b7f5-285f382fca99.png)
 
- Click "Edit" button to edit the product details
![image](https://user-images.githubusercontent.com/10927151/204085932-eb6a20fd-7f64-4519-b713-e8237b3b1ec1.png)

- Click "Delete" button to delete the product

- Click "Comment" button to be able to view product comments and be able to add, edit or delete comments.
![image](https://user-images.githubusercontent.com/10927151/204086036-773d286e-0f96-41b3-aae1-44ea663a9c6d.png)

- Click "Add Comment" button on the Comment modal to be able to add a comment to the list.
![image](https://user-images.githubusercontent.com/10927151/204086083-80dc5b1a-4f54-42b1-bb94-1a6f0b31920c.png)

- Click "Edit" button on the comment table to edit a comment.
![image](https://user-images.githubusercontent.com/10927151/204086135-c42151ad-9a59-4116-9ddb-c17d17b13748.png)

- Click "Delete" button on the comment table to delete a comment.

- Click on Stats on the navigation menu to navigate to Stats page.
![image](https://user-images.githubusercontent.com/10927151/204086245-daea73fa-2cb2-4460-a6ff-1b7e5ab607ea.png)

Clicking stats should take you to stats page.
![image](https://user-images.githubusercontent.com/10927151/204086281-d67e17e4-9dfc-4f96-8320-02a2b8abb998.png)

### Product Manager Console Application
This project is located under ProductManager.Client.
Update the "BaseURL" on appsettings.json for connection to ProductManager API 
```json
{
  "ProductManagerAPI": {
    "BaseURL": "http://localhost:5089"
  }
}
```
Once the appsettings is set you can run the application.

This will open a console which will prompt you to enter the path to the excel file data.
![image](https://user-images.githubusercontent.com/10927151/204086625-daa355aa-4b73-4ba1-ba0d-65eac5ed8949.png)

Once you enter the file then the application should read product data and send them as a bulk to Product Manager API.

