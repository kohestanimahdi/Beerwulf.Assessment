# Beerwulf Backend Developer Assessment
Thank you for taking the time to complete this coding assessment. We did some of the heavy lifting for you already by creating a project skeleton that you can use as a starting point. The assessment is purposefully simple which means we'll be paying special mind to best practises, code clarity and project structure. Try to stand out by delivering the best you can for this simple application.

## Assessment details
* Spend around **3 hours** on this assessment;
* Feel free to use `//comments` to show us the things you WOULD HAVE done if you had more time;
* Once you're done, zip the latest solution and send us the Google Drive, Dropbox, or OneDrive link;
* Your code must compile and run - VERY IMPORTANT!

## Coding assessment: Product Review API
Here at Beerwulf, we're always thinking about customer engagement. As such, we want to experiment on facilitating a way for customers to submit a review and rating for our beer products. 

#### Your task is to create a Review API to perform the following:
* Submitting a new review for a specific product;
* Getting a summary of reviews for a given product (the summary consists of the average score on reviews and percentage of recommendation);
* Listing all the reviews for a given product.

#### A `review` consists of the following:
* Score (range 1-5);
* Review title;
* Comment on the product;
* An indication of whether the customer would recommend this product.

#### A `product` consists of the following:
* Product ID;
* Name;
* Description;
* Price.

#### The project skeleton includes:
* ReviewController to start with - create any other controllers you require yourself;
* Swagger documentation configured in order to easily understand and interact with the endpoints.

### Requirements:
We would like your solution include the following:
* Data persistence. Feel free to use any solution for persistence, as long as it's **in-memory** (like **EF Core in-memory provider** or **Dapper**);
* Coding async all the way;
* Maintaining clean code on the Controller layer;
* Separating Service and Repository layers;
* Having unit tests is a bonus;
* Maintaining simple and clear documentation on the SwaggerUI;

## Last words
Please use our skeleton as a starting point, but don't be afraid to show off your solution arthitecture skills by creating a clever project scaffolding.

Best of luck, potential Wulfie! ;)
