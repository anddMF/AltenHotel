# AltenHotel
### API that handles the reservations from only one room in a Cancun's hotel.

<hr/>

### Brief
I tried to make the code as simple as possible with the constraints already presented, because with the experience I have in consulting and agile, this first scope will make a delivery that will lead to new features or maybe groomings that can change everything.

#### The division of the architecture:
- <b>Business:</b> contains the logic of the business and domain. For example: the validation if the user is trying to book more than three days.
- <b>Infra:</b> responsible for the outside interaction of the code. For example: has the DAO classes, which will be a reflection from the objects on the database used.
- <b>Entities:</b> classes that will be used on the code in general, can a business's class or just a cross cutting one.
- <b>Controllers:</b> will simply transfer the request parameters from the endpoint to the business layer.

Usually I separate the folder business and infra in class libraries, but to make a more simple and faster project to build, I used as a folder inside the web api project.