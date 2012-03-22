$(function () {
    $('#source-text').select();

    $('#app-info').corner('top 15px');
    $('#main-content').corner('20px');
    $('textarea').corner('5px');
    $('.type-list-item').corner('8px');

    var sendRequest = function (button, url) {
        var $button = $(button);
        $button.attr("disabled", "disabled");

        var sourceText = $('#source-text').val();
        var selectedType = $('input:radio:checked').val();

        var data = { text: sourceText, type: selectedType };
        $.getJSON(url, data, function (result) {
            $('#result-text').val(result || '');
        });

        $button.removeAttr("disabled");
    };

    $('#encode-button').click(function () {
        sendRequest(this, '/api/encode');
    });

    $('#decode-button').click(function () {
        sendRequest(this, '/api/decode');
    });
});