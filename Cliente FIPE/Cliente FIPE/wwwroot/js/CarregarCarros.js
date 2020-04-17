/// Eventos
$('document').ready(function () {
    CarregarCarros();
    
});

$('#txtVeiculos').focusin(function () {
    $('#divVeiculos').css('display', 'block');
});

$('#txtVeiculos').on('keyup', function () {
    CarregarCarros();
});

$('#slcMarcas').change(function () {
    $('#txtVeiculos').val('');
    CarregarCarros();
});

$("#txtVeiculos").change(function () {
    var dlstOptions = $("#dlstVeiculos")[0].options;
    var texto = $("#txtVeiculos").val();
    for (var i = 0; i < dlstOptions.length; i++) {
        if (dlstOptions[i].value == texto) {
            $('#hdnVeiculoId').val(dlstOptions[i].id);
            CarregarCarroModelos(dlstOptions[i].id);
        }
    }
});

$('#slcVeiculoModelos').change(function () {
    CarregarModeloDetalhes();
    $('#divVeiculos').css('display', 'none');
});


/// Funções
function CarregarCarros() {
    var urlBase = 'https://localhost:44311/';
    var res = $('#divVeiculos');
    var row = $('#divVeiculos > datalist');
    var textobusca = '';
    textobusca = $('#txtVeiculos').val();
    var idmarca = $('#slcMarcas').val();
    $.ajax({
        url: urlBase + 'Fipe/CarregarCarros/?veiculo=' + textobusca + '&idmarca=' + idmarca,
        type: 'POST',
        cache: false,
        async: true,
        data: { 'veiculo': textobusca, 'idmarca': idmarca },
        processData: false,
        contentType: false,
        success: function (result) {
            
            row.html('');

            $.each(result, function (index, value) {
                row.append('<option class=veiculosListaItem id=' + value.id + '>' + value.fipe_name + '</option>');
            });
        }
    });
    $('#divBotoes').css('display', 'none');
}

function CarregarCarroModelos(idveiculo) {
    var urlBase = 'https://localhost:44311/';
    
    var slcModelos = $('#slcVeiculoModelos');
    var idmarca = $('#slcMarcas').val();
    $.ajax({
        url: urlBase + 'Fipe/CarregarCarroModelos/?idveiculo=' + idveiculo + '&idmarca=' + idmarca,
        type: 'POST',
        cache: false,
        async: true,
        data: { 'idveiculo': idveiculo, 'idmarca': idmarca },
        processData: false,
        contentType: false,
        success: function (result) {
            slcModelos.html('');
            slcModelos.append('<option value=0 >Selecione o Modelo</option>');

            $.each(result, function (index, value) {
                slcModelos.append('<option value=' + value.id + '>' + value.name + '</option>');
            });
        }
    });
    $('#divBotoes').css('display', 'none');
}

function CarregarModeloDetalhes() {
    var idmodelo = $('#slcVeiculoModelos').val();
    if (idmodelo != '0') {

        var urlBase = 'https://localhost:44311/';
        var idmarca = $('#slcMarcas').val();
        var idveiculo = $('#hdnVeiculoId').val();
        var modeloDetalhes = $('#divModeloDetalhes');
        $.ajax({
            url: urlBase + 'Fipe/CarregarModeloDetalhes/?idveiculo=' + idveiculo + '&idmarca=' + idmarca + '&idmodelo=' + idmodelo,
            type: 'POST',
            cache: false,
            async: true,
            data: { 'idveiculo': idveiculo, 'idmarca': idmarca, 'idmodelo': idmodelo },
            processData: false,
            contentType: false,
            success: function (result) {
                modeloDetalhes.html('');
                modeloDetalhes.append('<label class="col-md-12 form-control">' + result.marca + '</label>');
                modeloDetalhes.append('<label id="veiculo" class="col-md-12 form-control">' + result.name + '</label>');
                modeloDetalhes.append('<label id="combustivel" class="col-md-12 form-control">' + result.combustivel + '</label>');
                modeloDetalhes.append('<label id="ano" class="col-md-12 form-control">' + result.ano_modelo + '</label>');
                modeloDetalhes.append('<label id="preco" class="col-md-12 form-control">' + result.preco + '</label>');
            }
        });

        $('#divBotoes').css('display', 'block');
    }
}

function AdicionarFavorito() {
    var veiculo = $('#veiculo').text();
    var combustivel = $('#combustivel').text();
    var ano = $('#ano').text();
    var preco = $('#preco').text();
    var favoritos = $('#rowFavoritos');
    var favExistente = favoritos.innerHTML;
    var favNovo = '';
    favNovo += '<div class="divFavorito form-control col-md-2" >';
    favNovo += '<button class="btn btn-danger float-right"  onclick="DeletarFavorito(this)"> x </button>';
    favNovo += '<label class="" style="display: block;">' + veiculo + '</label>';
    favNovo += '<label class="" style="display: block;">' + combustivel + '</label>';
    favNovo += '<label class="" style="display: block;">' + ano + '</label>';
    favNovo += '<label class="" style="display: block;">' + preco + '</label>';
    favNovo += '</div>';

    if (!favExistente) {
        favExistente = '';
    }
    
    favoritos.append(favNovo);
}

function DeletarFavorito(botao) {
    botao.closest('div').remove();
}
