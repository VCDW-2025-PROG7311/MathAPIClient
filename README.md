## Building the MathAPI Client

This repo shows the building of the MathAPI Client in VS Code.

A starting point for this app is the [MathApp](https://github.com/VCDW-2025-PROG7311/MathApp). 
A requirement for this API Client is the [MathAPI](https://github.com/VCDW-2025-PROG7311/MathAPI).

I strongly recommend building both first, so you understand how it all fits together before you break it up :-)

This API Client covers aspects like:
* Making POST requests to the MathAPI and sending through an object for processing
* Making GET requests to get history
* Making DELETE requests to clear history

We did do an API client before - [ESP Loadshedding](https://github.com/VCDW-2025-PROG7311/ESP_Client/)

**If you notice any errors or need to suggest improvements, please reach out to me!! I will be grateful**

It is recommended that you follow these steps in order:

1. [Creating your project](/Guides/CreatingYourProject.md)
1. [Reusing the various classes](/Guides/ReusingVariousClasses.md)
1. [Updating the Calculate() HttpPost to post to API](/Guides/UpdatingCalculate.md)
1. [Updating the History() HttpGet to read from API](/Guides/UpdatingHistory.md)
1. [Updating the Clear() to delete using API](/Guides/UpdatingClear.md)
1. Moving auth from client into API (you need to figure this out from the code)