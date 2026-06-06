public enum GameState
{
    // Saat pemain berada di menu utama sebelum mulai bermain.
    MainMenu,

    // Saat permainan sedang berlangsung.
    Playing,

    // Saat permainan dihentikan sementara (pause).
    Paused,

    // Saat permainan berakhir karena kalah atau kondisi game over.
    GameOver
}