$(document).ready(function () {

    $('#sendButton').on('click', function () {
        var data = {
            user: $('#userInput').val(),
            message: $('#messageInput').val()
        }
        $.ajax({
            url: 'home/SendMessage',
            data: data
        });
    })

    $('#sendProgress').on('click', function () {
        $.ajax({
            url: 'home/Processing'
        });
    })
})

