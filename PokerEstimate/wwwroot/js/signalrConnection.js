window.signalRConnection = {
    connection: null,

    initializeSignalRConnection: function () {
        return new Promise((resolve, reject) => {
            // Verifica se a conexão já está estabelecida ou em processo
            if (!this.connection || this.connection.state === signalR.HubConnectionState.Disconnected) {
                // Cria uma nova conexão SignalR
                this.connection = new signalR.HubConnectionBuilder()
                    .withUrl("/salaHub")
                    .build();

                // Inicia a conexão
                this.connection.start()
                    .then(() => resolve())
                    .catch(err => reject(`Erro ao iniciar a conexão: ${err}`));
            } else {
                // Caso já exista uma conexão ativa
                resolve();
            }
        });
    }
};