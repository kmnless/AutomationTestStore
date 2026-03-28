# Automation Test Store - QA Project

Automated UI testing solution for Automation Test Store(https://automationteststore.com) executing user scenarios via Selenium WebDriver.

## Task Description
The project covers three main User Cases (UC):
* **UC-1:** Test creation of a new account (filling form, accepting privacy policy, verifying successful login/welcome message).
* **UC-2:** Test validation messages on the Registration form (boundary values and invalid characters for the Login Name field).
* **UC-3:** Test Special offers (verifying all items on the Specials page have active discounts/new prices).

## Technology Stack
* **Language:** C# (.NET 8.0+)
* **Framework:** xUnit
* **Browser Automation:** Selenium WebDriver
* **Logging:** Serilog (Console & Rolling File)
* **Architecture:** Page Object Model (POM), Data-Driven Testing (DDT)
* **Design Patterns:** Singleton (Logger), Factory Method (Driver initialization)

## Features
* **Parallel Execution:** Tests run simultaneously for Google Chrome and Mozilla Firefox using xUnit Collections.
* **Flake-Free Design:** Implemented robust `WebDriverWait` mechanisms handling AJAX calls and `StaleElementReferenceExceptions` natively.
* **Dynamic Test Data:** Uses ticks-based unique identifiers to bypass duplicate account restrictions during repeated test runs.