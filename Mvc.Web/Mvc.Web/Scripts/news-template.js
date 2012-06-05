$(function () {
    var smiles = [
                { title: "Awesome", picture: "120px-Awesome.png", info: "fish", whichClass: 'first' },
                { title: "Awosome", picture: "120px-Awosome.png", info: "fish" },
                { title: "Eyebrow", picture: "120px-Eyebrow.jpg", info: "fish" },
                { title: "HowdoiAwesomeanswer", picture: "120px-HowdoiAwesomeanswer.png", info: "fish" },
                { title: "Shocksmilie", picture: "120px-Shocksmilie.png", info: "fish" },
                { title: "Squaresmilie", picture: "120px-Squaresmilie.gif", info: "fish" },
                { title: "Tardsmilie", picture: "120px-Tardsmilie.jpg", info: "fish", whichClass: 'last' }
            ];

    $("#articleTemplate").tmpl(smiles).appendTo("#news");
});