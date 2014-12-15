namespace Sporacid.Simplets.Webapp.Core.Security.Authorization
{
    using System;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface IContext<out TModel>
    {
        /// <summary>
        /// The context's associated actions.
        /// For example, if "(Actions | Actions.Update) && (Actions | Actions.Delete)" is true, then the
        /// action taken for the context is an update, coupled with a delete.
        /// </summary>
        Actions Actions { get; }

        /// <summary>
        /// The context's authorization level required to take this action.
        /// </summary>
        AuthorizationLevel RequiredAuthorizationLevel { get; }

        /// <summary>
        /// The model object associated with this context.
        /// </summary>
        TModel ContextObject { get; }

        /// <summary>
        /// The contextual relative url (i.e path) for this context.
        /// For example, a Hockey stats context which has the league name, the team name and the player name could
        /// return "/nhl/canadiens/subban".
        /// </summary>
        /// <returns>The relative url of this context.</returns>
        String RelativeUrl { get; }
    }
}