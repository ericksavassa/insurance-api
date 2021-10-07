## Github Reposiory:

https://github.com/ericksavassa/insurence-api

<br/>

## TASK 1:

### Assumption/Decision Made:
<br/>

* Fixed the rule to add insurence cost on product with sale value less than 500 and changed the IF to avoid check *toInsure.ProductTypeHasInsurance* all the time

![Task1.1](Task1_1.png "Task1.1")

* Changed the URL on *ControllerTestFixture* because ProductApi was using same port (5002), this way the "integrated" tests could run

![Task1.2](Task1_2.png "Task1.2")

* Add "integrated" test for the new implementation

![Task1.3](Task1_3.png "Task1.3")

### Reasons:

I only fixed the bug and kept the structure of the project and tests because in the Task 2 I will refactor the entire solution, splitting the layers, creating unit tests, etc.

