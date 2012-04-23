$(document).ready(function() {
    //var context = this;
    $("#compile").click(function () { clickCompile(); });
    $("#reset").click(clickReset);
    $("#puzzle-box").on("click", "div", clickSquare);

    var data = { values: [] };
    var squares = [];
    var valuesCopy = [];
    var numberOfSides;
    var count = 0;
    var winingSituation = false;
    var elementWidth;

    function counterForClickedSquares() {
        return (function() {
            return ++count;
        })();
    }

    function verifyInputData(value) {
        if (!isNaN(value) && value > 1) {
            return value;
        }
        return "undefined";
    }

    function numbering(numberOfSquares) {
        data.values.length = 0;
        for (var i = 0; i < numberOfSquares; i++) {
            data.values[i] = i + 1;
        }
        data.values[numberOfSquares - 1] = "";
        valuesCopy = data.values.slice();
    }

    function compilePuzzleBox(sidesNumber) {
        var sizeSideSquare = 50; //$(".square").css("width"); //ещё не созданные элементы
        var borderSquareLength = 1; //$(".square").css("border-width");
        var puzzleBox = $("#puzzle-box");
        puzzleBox.css("visibility", "visible");
        puzzleBox.css("width", sizeSideSquare * sidesNumber + sidesNumber * borderSquareLength * 2);
        puzzleBox.css("height", sizeSideSquare * sidesNumber + sidesNumber * borderSquareLength * 2);
        return sizeSideSquare + borderSquareLength * 2;
    }

    function clickSquare() {
        var value = +$(this).text();
        if (value !== 0 && !winingSituation) {
            var parameters = findEmptySquareParameters(value);

            if (parameters !== undefined) {
                replaceSquare(parameters);
            }
        }
    }

    function findIndexOfElement(value) {
        for (var i = 0, length = valuesCopy.length; i <= length; i++) {
            if (valuesCopy[i] == value)
                return i;
        }
    }

    function replaceArrayValues(array, indexEmptyElement, indexClickedElement) {
        var temp = array[indexClickedElement];
        array[indexClickedElement] = array[indexEmptyElement];
        array[indexEmptyElement] = temp;
    }

    function findEmptySquareParameters(value) {
        var index = findIndexOfElement(value);
        var array = [index - numberOfSides, index + 1, index + numberOfSides, index - 1];
        for (var i = 0, length = array.length; i < length; i++) {
            if (valuesCopy[array[i]] === '' && checkSquaresIndexes(array[i], index)) {
                switch (i) {
                    case 0 : return [array[i], index, 'top', 'plus']; break;
                    case 1 : return [array[i], index, 'left', 'minus']; break;
                    case 2 : return [array[i], index, 'top', 'minus']; break;
                    case 3 : return [array[i], index, 'left', 'plus']; break;
                }
            }
        }
    }

    function checkSquaresIndexes(indexEmptyElement, indexClickedElement) {
        return !((indexClickedElement + 1) % numberOfSides === 0 && indexEmptyElement % numberOfSides === 0
            || indexClickedElement % numberOfSides === 0 && (indexEmptyElement + 1) % numberOfSides === 0);
    }

    function replaceSquare(parameters) {
        var coordinate = parameters[2] /*'left'*/;
        var firstOperator = parameters[3] /*'minus'*/;
        var squareWithEmptyValue = $(squares[parameters[0]]);
        var squareClicked = $(squares[parameters[1]]);

        switch(firstOperator) {
            case 'minus':
//                squareWithEmptyValue.animate({coordinate: parseInt(squareWithEmptyValue.css(coordinate)) - elementWidth + 'px'}, 500);
//                squareClicked.animate({coordinate: parseInt(squareClicked.css(coordinate)) + elementWidth + 'px'}, 500);
                squareWithEmptyValue.css(coordinate, parseInt(squareWithEmptyValue.css(coordinate)) - elementWidth);
                squareClicked.css(coordinate, parseInt(squareClicked.css(coordinate)) + elementWidth);
                break;
            case 'plus':
                squareWithEmptyValue.css(coordinate, parseInt(squareWithEmptyValue.css(coordinate)) + elementWidth);
                squareClicked.css(coordinate, parseInt(squareClicked.css(coordinate)) - elementWidth);
                break;
        }

        replaceArrayValues(valuesCopy, parameters[0], parameters[1]);
        replaceArrayValues(squares, parameters[0], parameters[1]);
        count = counterForClickedSquares();
        winingSituation = verifyWinningSituation();
        numberingOfClicks();
    }

    function squareHover() {
        $('.square').hover(
            function(){ $(this).addClass('hover') },
            function(){ $(this).removeClass('hover') }
        );
    }

    function numberingOfClicks() {
        var clicksCounter = $('#clicksCounter');
        clicksCounter.text(count);
        if (winingSituation) {
            clicksCounter.css('background-color', '#32CD32');
            clicksCounter.css('color', '#f0fff0');
        }
        else {
            clicksCounter.css('background-color', 'white');
            clicksCounter.css('color', 'black');
        }
    }

    function getRandom() {
        return Math.round(Math.random() * 3);
    }

    function mixing() {
        for (var i = 0; i < 200; i++) {
            var parameters;
            var index = findIndexOfElement('');
            var array = [index - numberOfSides, index + 1, index + numberOfSides, index - 1];

            var rand = getRandom();
            if (valuesCopy.length > array[rand] && checkSquaresIndexes(index, array[rand])) {
                switch (rand) {
                    case 0 : parameters = [index, array[rand], 'top', 'minus']; break;
                    case 1 : parameters = [index, array[rand], 'left', 'plus']; break;
                    case 2 : parameters = [index, array[rand], 'top', 'plus']; break;
                    case 3 : parameters = [index, array[rand], 'left', 'minus']; break;
                }
            }

            if (parameters) {
                var coordinate = parameters[2] /*'left'*/;
                var firstOperator = parameters[3] /*'minus'*/;
                var squareEmpty = $(squares[parameters[0]]);
                var squareWithNumber = $(squares[parameters[1]]);

                switch(firstOperator) {
                    case 'minus':
                        squareEmpty.css(coordinate, parseInt(squareEmpty.css(coordinate)) - elementWidth);
                        squareWithNumber.css(coordinate, parseInt(squareWithNumber.css(coordinate)) + elementWidth);
                        break;
                    case 'plus':
                        squareEmpty.css(coordinate, parseInt(squareEmpty.css(coordinate)) + elementWidth);
                        squareWithNumber.css(coordinate, parseInt(squareWithNumber.css(coordinate)) - elementWidth);
                        break;
                }

                replaceArrayValues(valuesCopy, parameters[0], parameters[1]);
                replaceArrayValues(squares, parameters[0], parameters[1]);
            }
        }
    }

    function verifyWinningSituation() {
        var counter = 0;
        for (var i = 0, length = valuesCopy.length; i < length; i++) {
            if (valuesCopy[i] === data.values[i]) {
                counter++;
            }
        }
        return counter === length;
    }

    function clickReset() {
        resetValues();
        mixing();
    }

    function resetValues() {
        count = 0;
        winingSituation = false;
        numberingOfClicks();
    }

    function clickCompile() {
        var temp = +$("#textInput").val();
        numberOfSides = temp;
        //numberOfSides = numberOfSides === temp || numberOfSides !== 0 ? 0 : temp;
        if (verifyInputData(numberOfSides) !== "undefined") {
            resetValues();
            $("#puzzle-box").empty();
            numbering(Math.pow(numberOfSides, 2));
            elementWidth = compilePuzzleBox(numberOfSides);
            $("#squareTemplate").tmpl(data).appendTo("#puzzle-box");
            squares = $(".square").get();
            mixing();
            squareHover();
            //(function() { return (function() { return numberOfSides; })() })();
        }
    }
});
