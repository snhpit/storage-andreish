(function($){

    function Room(){
        if (!(this instanceof Room)) {
            return new Room();
        }
        if (typeof Room.counter == 'undefined') {
            Room.counter = 0;
        }

        Room.counter = Room.counter + 1;
    }

    var p1 = new Room;
    console.log(Room.counter);
    var p2 = new Room();
    console.log(Room.counter);
    var p3 = Room;
    console.log(Room.counter);  // why 2, not 3 ?






    var Person = function(name) {
        this.name = name;
        //this.hi /*= Person.hi*/;
        /*return {
            hi : function(strangerName) {
                return "Hi, " + strangerName;
            }
        }*/
    };

    Person.hi = function(strangerName) {
        return "Hi, " + strangerName;
    };

    Person.age = 19;

    console.log(Person.hi("Mahmed"));
    console.log(Person.age);

    Person.prototype.say = function(){
        return "I'm " + this.name;
    };

    var newPerson = new Person("Volodia cho");
    //console.log(newPerson.hi("Gogi"));  //isn't a func
    //console.log(newPerson.age); //undefined
    console.log(newPerson.say());

    for(var i in Person) {
        if (Person.hasOwnProperty(i)) {
            console.log("own property of class Person " + i);
        }
    }

    for(var i1 in newPerson) {
        if (newPerson.hasOwnProperty(i1)) {
            console.log("own property of instance of Person " + i1);
        }
    }







    var someObject = {
        name : "someObject",
        array: [],
        add: function(parameter) {
            this.array.push(parameter);
        },
        getName : function() {
            return this.name;
        }
    };

    var personObject = {
        hands : 2,
        legs : 2,
        head : 1
    };

    var extendObjectTestAdded = $.extend(true, {}, someObject);
    extendObjectTestAdded.add('test');

    console.log(extendObjectTestAdded.getName());
    console.log(extendObjectTestAdded);
    console.log(someObject);






    (function(){
        this.Markup = (function(){
            function Markup(){
                console.log("Hi from Markup");
            }
            return Markup;
        })();

        $(function(){
            return new Markup;
        });
    }).call(this);

})(jQuery); // jQuery not determined