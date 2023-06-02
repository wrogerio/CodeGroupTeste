$(document).ready(function (e) {
    gerarEscalacaoHTML()
})

function getJogoDetails() {
    var jogoId = window.location.pathname.toLowerCase().split('index/')[1];
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: "/Partida/GetPartidaDetails",
            type: "POST",
            data: { jogoId: jogoId },
            success: function (data) {
                resolve(JSON.parse(data));
            },
            error: function (data) {
                reject(data);
            }
        });
    });
}
function SortearTimes(jogadores, qtdPorTime) {
    jogadores = jogadores.filter(j => j.IsConfirmado);

    var qtdMinima = qtdPorTime * 2;
    var qtdConfirmada = jogadores.filter(j => j.IsConfirmado).length
    var isValidQtdPlayers = qtdConfirmada >= qtdMinima;
    var timeSize = Math.ceil(jogadores.length / qtdPorTime);
    var times = []

    // checar se a quantidade de jogadores é suficiente para formar os times
    if (!isValidQtdPlayers) {
        console.log('Quantididade minima de jogadores não atingida');
        return false
    }

    // separar os goleiros dos jogadores de linha
    var goleiros = jogadores.filter(j => j.IsGoleiro);
    var linha = jogadores.filter(j => !j.IsGoleiro);

    // Se a quantidade de goleiros for menor que a quantidade de times, não é possivel formar os times
    if (goleiros.length < timeSize) {
        $('#divMensagens').append(`<h3 class='text-danger'>Quantidade de goleiros insuficiente</h3>`);
        return false
    }

    // os goleiros precisam ficar com nivel 0, para nao interferir na separacao dos times
    goleiros = goleiros.map(g => { return { ...g, Nivel: 0 } });

    // se a quantidade de goleiros for maior que a quantidade de times, pegar os goleiros a partir da diferenca e jogar no array de linha
    if (goleiros.length > timeSize) {
        var diferenca = goleiros.length - timeSize;
        for (let i = 0; i < diferenca; i++) {
            // pegar o nome do goleiro na ultima posicao do array
            var nomeGoleiro = goleiros[goleiros.length - 1].Nome;
            $('#divMensagens').append(`<h4 class='text-danger'>O goleiro ${nomeGoleiro} precisou ir para a linha. Ja tinha goleiro suficiente</h4>`);
            linha.push(goleiros.pop());
        }
    }

    // ordenar os jogadores por nivel decrescente
    linha = linha.sort((a, b) => b.Nivel - a.Nivel);

    console.log(linha);

    // distribuir cada goleiro em um time
    goleiros.forEach((goleiro, index) => {
        var timeIndex = index % timeSize;
        if (!times[timeIndex]) times[timeIndex] = []
        times[timeIndex].push({ timeId: timeIndex, ...goleiro });
    });

    // distribuir os jogadores nos times, cada time ganha um jogador de Nivel mais alto
    linha.forEach((jogador, index) => {
        console.log(jogador)
        var timeIndex = index % timeSize;
        times[timeIndex].push({ timeId: timeIndex, ...jogador });
    });

    // obter a quantidade de times gerados
    const QtdTimesGerados = times.map((time, index) => index).length;

    // percorrer cada time que foi gerado
    for (let i = 0; i < QtdTimesGerados; i++) {
        // se o time atual tiver menos jogadores que o tamanho definido, pega o jogador do ultimo grupo e adiciona no time atual
        if (times[i].length < qtdPorTime) {
            // remove o ultimo elemento do ultimo grupo
            var lastElement = times[times.length - 1].pop();
            // adiciona o ultimo elemento no time atual
            times[i].push(lastElement);
        }
    }

    // checar a soma dos niveis de cada time
    var sum = times.map(time => time.reduce((acc, cur) => acc + cur.Nivel, 0));

    // adicionar a soma dos niveis de cada time no objeto
    times = times.map((time, index) => {
        return { timeId: index, jogadores: time, soma: sum[index] }
    });

    return times;
}

async function montarEscalacao() {
    var partidaDetails = await getJogoDetails();
    var jogadores = partidaDetails.Jogadores;
    var timesSorteados = SortearTimes(jogadores, partidaDetails.QtdPorTime);
    return timesSorteados;
}

async function gerarEscalacaoHTML() {
    $("#divEscalacao").empty();

    var modeloHTML_Start = `<div class="col"><h3 class='text-danger'>$titulo</h3><ul class="list-group">`;
    var modeloHTML_jogador = `<li class="list-group-item">$jogadorNome - $jogadorPontos</li>`
    var modeloHTML_End = `</ul></div>`;

    var escalacao = await montarEscalacao();

    escalacao.forEach(time => {
        var html = modeloHTML_Start;
        html = html.replace('$titulo', `Time ${time.timeId + 1} - ${time.soma} pontos`);
        time.jogadores.forEach(jogador => {
            html += modeloHTML_jogador.replace('$jogadorNome', jogador.Nome);
            html = html.replace('$jogadorPontos', jogador.Nivel);
        });
        html += modeloHTML_End;
        $("#divEscalacao").append(html);
    });
    console.log(escalacao)
}