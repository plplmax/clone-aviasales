$(function () {
    let originIata = $('input[name = origin_iata]'),
        destinationIata = $('input[name = destination_iata]'),
        departDate = $('input[name = depart_date]'),
        returnDate = $('input[name = return_date]'),
        originName = $('.twidget-origin .twidget-pseudo-name'),
        destinationName = $('.twidget-destination .twidget-pseudo-name');

    $('#search-form').submit(function (event) {
        event.preventDefault();

        if (isFormValid()) {
            let originIataValue = originIata.val(),
                destinationIataValue = destinationIata.val(),
                departDateValue = departDate.val(),
                returnDateValue = returnDate.val();

            console.log(departDateValue);

            let test = $(this).serialize();

            console.log(test);

            $.ajax({
                type: 'GET',
                url: 'api/ticket',
                dataType: 'json',
                data: test,
                success: function (data) {
                    loadTickets(data);
                },
                error: function (error) {
                    $('.not-found').css("display", "block");
                }
            });
        }
    });

    function isFormValid() {
        return originIata.val() && destinationIata.val() && departDate.val() /*&& returnDate.val()*/;
    }

    function loadTickets(data) {
        let tickets = data.data;
        let cities = data.cities;
        let airlines = data.airlines;

        for (obj of tickets) {
            var durat = Number(obj.duration);
            var d = Math.floor(durat / (60 * 24));
            var h = Math.floor(durat % (60 * 24) / 60);
            var m = Math.floor(durat % 60)

            let origin = cities[obj.origin].name;
            let destination = cities[obj.destination].name;
            let price = obj.price;
            let durationHours = obj.duration / 60;
            let durationsMinutes = obj.duration % 60;
            let link = obj.link;
            let departDate = new Date(Date.parse(obj.departure_at));
            let departDateFinish = new Date(departDate.getTime() + durat * 60 * 1000);
            let returnDate = new Date(Date.parse(obj.return_at));

            let html = '<div class="card border-light rounded-15 ms-4 mb-4">' +
                '<div class="card-body d-flex p-0">' +
                '<div class="flight-price border-end p-3 d-flex align-items-center">' +
                '<a class="btn btn-primary py-1 px-5" href="https://aviasales.ru' + link + '">Купить за <p>' + price + ' BYN</p></a>' +
                '</div>' +
                '<div class="flight-content ms-3 p-3">' +
                '<div class="flight-content__airlines">' +
                '<img src="https://pics.avs.io/al_square/36/36/' + obj.airline + '@2x.png" data-bs-toggle="tooltip" data-bs-placement="top" title="' + airlines[obj.airline].name + '" class="custom-img me-2 rounded-pill">' +
                '</div>' +
                '<div class="flight-content__description d-flex mt-3 align-items-center">' +
                '<div class="flight-content__origin lh-base">' +
                '<p class="fs-3">' + new Intl.DateTimeFormat('ru-RU', { hour: '2-digit', minute: '2-digit', timeZone: cities[obj.origin].timezone }).format(departDate) + '</p>' +
                '<p class="fs-6 text-secondary">' + origin + '</p>' +
                '<p class="fs-6 text-secondary">' + new Intl.DateTimeFormat('ru-RU', { weekday: 'short', day: 'numeric', month: 'short', year: 'numeric', timeZone: cities[obj.origin].timezone }).format(departDate) + '</p>' +
                '</div>' +
                '<div class="flight-content__duration mx-5 text-center">' +
                '<span>В пути: ' + (d ? d + 'д ' : "") + (h ? h + 'ч ' : "") + (m ? m + 'м' : "") + '</span>' +
                '<p>(пересадок: ' + obj.transfers + ')</p>' +
                '</div>' +
                '<div class="flight-content__destination text-end lh-base">' +
                '<p class="fs-3">' + new Intl.DateTimeFormat('ru-RU', { hour: '2-digit', minute: '2-digit', timeZone: cities[obj.destination].timezone }).format(departDateFinish) + '</p>' +
                '<p class="fs-6 text-secondary">' + destination + '</p>' +
                '<p class="fs-6 text-secondary">' + new Intl.DateTimeFormat('ru-RU', { weekday: 'short', day: 'numeric', month: 'short', year: 'numeric', timeZone: cities[obj.destination].timezone }).format(departDateFinish) + '</p>' +
                '</div>' +
                '</div>' +
                '<div class="flight-content__description d-flex mt-3 align-items-center border-top pt-3">' +
                '<div class="flight-content__origin lh-base flex-basis-100">' +
                '<p class="fs-3">' + new Intl.DateTimeFormat('ru-RU', { hour: '2-digit', minute: '2-digit', timeZone: cities[obj.destination].timezone }).format(returnDate) + '</p>' +
                '<p class="fs-6 text-secondary">' + destination + '</p>' +
                '<p class="fs-6 text-secondary">' + new Intl.DateTimeFormat('ru-RU', { weekday: 'short', day: 'numeric', month: 'short', year: 'numeric', timeZone: cities[obj.destination].timezone }).format(returnDate) + '</p>' +
                '</div>' +
                '<div class="flight-content__duration mx-5 text-center flex-basis-100">' +
                '<img src="./images/help_black_48dp.svg" data-bs-toggle="tooltip" data-bs-placement="top" title="При покупке указано время в пути" class="custom-img me-2 rounded-pill">' +
                '</div>' +
                '<div class="flight-content__destination text-end lh-base flex-basis-100">' +
                '<img src="./images/help_black_48dp.svg" data-bs-toggle="tooltip" data-bs-placement="top" title="При покупке указано время прибытия" class="custom-img me-2 rounded-pill">' +
                '<p class="fs-6 text-secondary">' + origin + '</p>' +
                '<img src="./images/help_black_48dp.svg" data-bs-toggle="tooltip" data-bs-placement="top" title="При покупке указана дата прибытия" class="custom-img me-2 rounded-pill">' +
                '</div>' +
                '</div>' +
                '</div>' +
                '</div>' +
                '</div>';

            $('.flight').append(html);
        }

        //todo: Сделать код красивее

        var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
        var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
            return new bootstrap.Tooltip(tooltipTriggerEl)
        })
    }
})