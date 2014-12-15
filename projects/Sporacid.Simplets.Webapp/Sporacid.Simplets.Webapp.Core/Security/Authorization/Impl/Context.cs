namespace Sporacid.Simplets.Webapp.Core.Security.Authorization.Impl
{
    using System;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class Context<TModel> : IContext<TModel>
    {
        /// <summary>
        /// The context's associated actions.
        /// For example, if "(Actions | Actions.Update) && (Actions | Actions.Delete)" is true, then the
        /// action taken for the context is an update, coupled with a delete.
        /// </summary>
        public Actions Actions { get; set; }

        /// <summary>
        /// The context's authorization level required to take this action.
        /// </summary>
        public AuthorizationLevel RequiredAuthorizationLevel { get; set; }

        /// <summary>
        /// The model object associated with this context.
        /// </summary>
        public TModel ContextObject { get; set; }

        /// <summary>
        /// The contextual relative url (i.e path) for this context.
        /// For example, a Hockey stats context which has the league name, the team name and the player name could
        /// return "/nhl/canadiens/subban".
        /// </summary>
        public string RelativeUrl { get; private set; }
    }
}