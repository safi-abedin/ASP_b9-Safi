

new window.stacksEditor.StacksEditor(
    document.querySelector("#editor-example-2"),
    document.querySelector("#editor-content-2").value,
    {
        parserFeatures: {
            tables: true,
        },
    }
);


$(function () {
    $("#answerForm").submit(function (e) {

        var qid = $("#questionId").val();
        var textArea = document.getElementById('editor-content-2');
        var textAreaValue = textArea.value;
        console.log(qid);

        $.ajax({
            url: "/User/Questions/Answer",
            type: "POST",
            dataType: "json",
            data: { id: gid, Body: textAreaValue },
            success: function (response) {
                alert(response.Message);
                $("#answerContent").val("");
            },
            error: function (xhr, status, error) {
                alert("An error occurred while posting the answer.");
                console.error(error);
            }
        });
    });
});