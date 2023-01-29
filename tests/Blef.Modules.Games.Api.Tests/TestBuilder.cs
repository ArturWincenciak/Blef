namespace Blef.Modules.Games.Api.Tests;

internal sealed class TestBuilder
{
    private readonly List<Func<Task>> _actions = new();
    private HttpClient _client = null!;

    private Guid _gameId;
    private Guid _knuthPlayerId;
    private Guid _grahamPlayerId;
    private Guid _riemannPlayerId;
    private Guid _conwayPlayerId;

    async internal Task Build()
    {
        _client = new BlefApplicationFactory()
            .CreateClient();

        foreach (var action in _actions)
            await action();
    }

    internal TestBuilder WithNewGame()
    {
        _actions.Add(MakeNewGame);
        return this;

        async Task MakeNewGame() =>
            _gameId = await _client.MakeNewGame();
    }

    internal TestBuilder WithJoinPlayer(WhichPlayer whichPlayer)
    {
        _actions.Add(JoinPlayer);
        return this;

        async Task JoinPlayer()
        {
            var playerId = await _client.JoinPlayer(_gameId, nick: whichPlayer.ToString());
            SetPlayerId(whichPlayer, playerId);
        }
    }

    internal TestBuilder WithGetPlayerCard(WhichPlayer whichPlayer)
    {
        _actions.Add(GetPlayerCards);
        return this;

        async Task GetPlayerCards() =>
            await _client.GetPlayerCards(_gameId, GetPlayerId(whichPlayer));
    }

    internal TestBuilder WithBid(WhichPlayer whichPlayer, string bid)
    {
        _actions.Add(Bid);
        return this;

        async Task Bid() =>
            await _client.Bid(_gameId, playerId: GetPlayerId(whichPlayer), bid);
    }

    internal TestBuilder WithCheck(WhichPlayer whichPlayer)
    {
        _actions.Add(Check);
        return this;

        async Task Check() =>
            await _client.Check(_gameId, playerId: GetPlayerId(whichPlayer));
    }

    internal TestBuilder WithGetGameFlow()
    {
        _actions.Add(GetGameFlow);
        return this;

        async Task GetGameFlow() =>
            await _client.GetGameFlow(_gameId);
    }

    private Guid GetPlayerId(WhichPlayer whichPlayer) =>
        whichPlayer switch
        {
            WhichPlayer.Knuth => _knuthPlayerId,
            WhichPlayer.Graham => _grahamPlayerId,
            WhichPlayer.Riemann => _riemannPlayerId,
            WhichPlayer.Conway => _conwayPlayerId,
            _ => throw new ArgumentOutOfRangeException(nameof(whichPlayer))
        };

    private void SetPlayerId(WhichPlayer whichPlayer, Guid playerId)
    {
        if (whichPlayer == WhichPlayer.Knuth)
            _knuthPlayerId = playerId;
        else if (whichPlayer == WhichPlayer.Graham)
            _grahamPlayerId = playerId;
        else if (whichPlayer == WhichPlayer.Riemann)
            _riemannPlayerId = playerId;
        else if (whichPlayer == WhichPlayer.Conway)
            _conwayPlayerId = playerId;
        else
            throw new ArgumentOutOfRangeException(nameof(whichPlayer));
    }

    internal enum WhichPlayer
    {
        None,
        Knuth,
        Graham,
        Riemann,
        Conway
    }
}