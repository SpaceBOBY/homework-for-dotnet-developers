# Challenge

Task is to develop a simple library that handles order placement. 

The functionality should cover following:
* Place an order for a DNA testing kit providing desired amount of kits, customer id and expected delivery date
* List all customer orders

Discount rules:
* DNA testing kit base price is 98.99 EUR
* Starting from 10 item order there is 5% discount
* From 50 item order there is 15% discount

Reject order placement when:
* Delivery date is not in future
* Desired amount is not positive round number
* Desired amount is greater than 999

Considerations for the design:
* In future we will have many kit variants
* Each kit variant may have a different base price

You can skip authentication and authorization. 
API or UI or DB is not required.

## Technical requirements
* Use c# programming language
* Use any testing framework for tests
* You can choose whatever tools and libraries you prefer

## What gets evaluated
* Accordance to business requirements described above
* Code quality (we value design guided by tests)
* Checkout-and-run convenience

## Have fun! 
