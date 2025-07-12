## E-Commerce Cart System

## Project Summary
A **C# console application** that simulates an e-commerce shopping cart system.  
This project demonstrates **object-oriented design principles**, enabling flexible product behaviors such as expiration and shipping.

The system handles:
- Product management with different features
- Customer transactions
- Shipping calculations and order processing

---

## Test Cases

### Screenshots of Output
![Output Screenshot 1](assets/screenshot1.png)  
![Output Screenshot 2](assets/screenshot2.png)

---

## Design Principles Used

### Composition Over Inheritance
- Products are composed using interfaces like `IExpirable` and `IShippable`
- Avoids rigid inheritance hierarchies
- Promotes flexible feature combinations
- `Product` contains components for both expiration and shipping logic

### Interface Segregation Principle (ISP)
- Small, focused interfaces:
  - `IExpirable` – handles expiration logic
  - `IShippable` – handles shipping properties
  - `IShippingService` – handles shipping cost calculation
- Classes implement only the interfaces they need

---

##  Requirements to Run

- .NET Framework 4.7.2 or higher  
- Visual Studio 2017 or higher (recommended)  
- C# 7.0 or higher  

