# Beerwulf Developer Assessment
We'd like to take you through our coding assessment that we created for our developer candidates. Thanks for taking your time to do this and we also made sure you don't spend more than 1 hour on this assessment.
## Once you complete the assessment
You can use our skeleton for this assessment. For simplicity reasons, we prefer everything under web project but that shouldn't stop you from showing your solution arthitecture skills by maintaining a clever scaffolding. You can zip the latest solution and send Drive/Dropbox/OneDrive link to us.
## Coding Test
At Beerwulf, we might want to experiment about a functionality on our product detail page to have product reviews by customers.
Your test is to create API endpoints for reviews to perform:
* Creating a new review on a specific product.
* A summary of reviews on a specific product. (Summary consists of; average score on reviews, percentage of recommendation)
* Listing all reviews on a specific product.
For further context on this a review consists these informations:
* Score (range 1-5)
* Title of review
* Comments on product
* An information based on if customer would recommend it to friends.

Project skeleton will include:
* ReviewController to start with.
* Swagger documentation configured in order to easily understand and interact with endpoints.

**We recommend you to use EF Core in-memory provider as persistence solution**, if you think there's another solution that you feel more comfortable with, as long as it's in-memory, you can go ahead.

### Additional Information
We would like your solution to fulfill these;
* Coding async all the way.
* Maintaining a clean code on controller layer.
* Seperating service and repository layers.
* Having unit tests.
* Maintaining a simple documentation on Swagger.
* Your code must compile and run.