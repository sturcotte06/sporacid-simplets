var api = {
    client: {
        utility: {
            // No-op operation to test credentials and rights.
            noop: function(auth) {
                return restCall(buildUrl(apiUrl, "no-op"), operations.get(), auth ? auth : buildTokenAuthHeader());
            },
            // Get all operation to get the api description.
            apiDescription: function() {
                return restCall(buildUrl(apiUrl, "describe-api"), operations.get());
            },
            // Get all operation to get the api's entities description.
            entitiesDescription: function() {
                return restCall(buildUrl(apiUrl, "describe-entities"), operations.get());
            }
        },
        enumerations: {
            // Get all operation for concentrations enumeration.
            concentrations: function() {
                return restCall(buildUrl(apiUrl, "enumeration/concentrations"), operations.get());
            },
            // Get all operation for contacts types enumeration.
            typesContacts: function() {
                return restCall(buildUrl(apiUrl, "enumeration/types-contacts"), operations.get());
            },
            // Get all operation for statuts suivies enumeration.
            statutsSuivies: function() {
                return restCall(buildUrl(apiUrl, "enumeration/statuts-suivies"), operations.get());
            },
            // Get all operation for unites enumeration.
            unites: function() {
                return restCall(buildUrl(apiUrl, "enumeration/unites"), operations.get());
            },
            // Get all operation for unites enumeration.
            typesCommanditaires: function() {
                return restCall(buildUrl(apiUrl, "enumeration/types-commanditaires"), operations.get());
            }
        },
        userspace: {
            // Get operation for the profil of the current user.
            profil: function(identity, auth) {
                return restCall(buildUrl(apiUrl, identity, "profil"), operations.get(), auth ? auth : buildTokenAuthHeader());
            },
            // Update operation for the profil of the current user.
            updateProfil: function (identity, profil, auth) {
                return restCall(buildUrl(apiUrl, identity, "profil"), operations.update(), auth ? auth : buildTokenAuthHeader(), profil);
            },
            // Get operation for the club subscriptions of the current user.
            subscribedClubs: function (auth) {
                
            }
        }
    }
};