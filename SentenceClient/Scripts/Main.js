function addWord(x) {
    var word = $(x).find(":selected").text();
    var existing = $("#SentenceBox").val();
    $("#SentenceBox").val(existing + ' ' + word);
}

$("#SaveSentence").click(function (e) {
    e.preventDefault();
    var sentence = $("#SentenceBox").val().trim();
    if (sentence.length > 0) {
        var correct = cleanUpSentence(sentence);
        if ($("li:contains('" + correct + "')").length) {
            alert("That sentence already exists");
        }
        else {
            $("#SentenceContainer").append('<li>' + correct + '</li>');
            var baseAddress = window.location.href;
            $.ajax({
                type: "POST",
                url: baseAddress + 'Home/StoreSentence',
                data: { fullSentence: correct },
                success: function (data) {
                    if (data) {
                        $('#SuccessModal').modal('show');
                    } else {
                        $("#ErrorValue").html('Internal backend server error occurred and your sentence could not be stored.');
                        $("#DynamicErrorModel").modal('show');
                    }
                },
                error: function () {
                    $("#ErrorValue").html('Error connection to controller action');
                    $("#DynamicErrorModel").modal('show');
                }
            });
        }
        $("#SentenceBox").val('');
    } else {
        $("#ErrorValue").html('No items were added in the sentence bar');
        $("#DynamicErrorModel").modal('show');
    }
});

$("#ClearSentence").click(function() {
    $("#SentenceBox").val('');
});

function cleanUpSentence(sentence) {
    var firstCharacter = sentence.substring(0, 1);
    firstCharacter = firstCharacter.toUpperCase();
    var sentenceRemainder = sentence.substring(1);
    var returnSentence = firstCharacter + sentenceRemainder + '.';
    return returnSentence;
}

