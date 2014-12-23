/// <summary>
/// Bootstraps the application. 
/// After this call, the application should be useable.
/// </summary>
function bootstrapApplication() {
    // Create a new rest client.
    var simpletsRestClient = new SimpletsRestClient();

    // Force the user to login.
    requestLogin(simpletsRestClient);

    // Retrieve the user's contexts.
    var availableContexts = [];
    simpletsRestClient.stores.profil.getAvailableContexts().whenDone(function(client, contexts) {
        for (var iContext = 0; iContext < contexts.length; iContext++) {
            alert(contexts[iContext].Entity.Nom);
            // availableContexts.push(contexts[iContext].Entity);
        }
    }).do();
}

/// <summary>
/// Request the user to log in. 
/// This method will not exit until the user logs in successfully.
/// </summary>
/// <param "name="simpletsRestClient">A rest client.</param>
function requestLogin(simpletsRestClient) {
    // Try to login. Do it until 
    var loginSuccessful = false;
    while (!loginSuccessful) {
        // Retrieve the credentials of the user.
        var credentials = retrieveCredentials();

        try {
            simpletsRestClient.login(credentials.username, credentials.password);
            loginSuccessful = true;
        } catch (ex) { throw ex; }
    }
}

/// <summary>
/// Retrieves the credentials from the user.
/// </summary>
function retrieveCredentials() {
    return {
        username: "AJ50440",
        password: "AOPbgtZXD666"
    };
}