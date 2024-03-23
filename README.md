# Parcel2Go Technical Test 2024
**Last Updated:** 2024-03-21

## Introduction
You are tasked with developing a checkout system for a leading parcel shipping company. Services are categorized with alphabetical identifiers (A, B, C, D, etc.). Your system must handle individual service rates and discounts for bulk purchases.

## Service Pricing and Promotions
Each service has a unique letter identifier and a price. Special rates are available for customers buying multiples of the same service, designed to encourage bulk purchases. Here's our current pricing and promotions:

- **Service A:** £10 each or 3 for £25
- **Service B:** £12 each or 2 for £20
- **Service C:** £15 each
- **Service D:** £25 each
- **Service F:** £8 each or 2 for £15

## Checkout System Requirements
The checkout system is intuitive, processing services in any sequence and automatically determining the total cost by employing the most favorable pricing strategy, which includes individual pricing and multipurchase discounts.

### Suggested interface for the checkout process:
```csharp
interface ICheckout
{
    void Scan(string service); // Adds a service to the checkout
    int GetTotalPrice(); // Calculates the total price, applying the best discount option
}
```

### Examples Illustrating Discount Application

**Example 1: Multipurchase Discount Advantage**
- Customer's Cart: 2 x Service B
- Standard Pricing: £12 each
- Special Offer for B: 2 for £20
- Total without discount: 2 x £12 = £24
- With multipurchase discount: £20
- Decision: The system applies the multipurchase discount, totaling £20, as it offers better savings.

**Example 2: No Multipurchase Discount**
- Customer's Cart: 1 x Service F and 1 x Service C
- Standard Pricing: Service F at £8 and Service C at £15
- No applicable multipurchase discount.
- Total without discount: £8 + £15 = £23
- Decision: The system calculates the total as £23 due to no available multipurchase discounts.

**Example 3: Mix of Discounts and No Discount**
- Customer's Cart: 2 x Service F, 1 x Service B
- Standard Pricing: F @ £8 each, B @ £12 each
- Special Offer for F: 2 for £15
- Total without discount: 2 x £8 + 1 x 12 = £28
- With multipurchase discount: £27
- Decision: The system applies the multipurchase discount for the F products, no discount on B as only 1 bought reducing the total to £27.

Please make sure your code works against the provided example scenarios above.
