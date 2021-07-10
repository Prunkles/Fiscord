namespace Fiscord

open System.Threading.Tasks
open Discord
open Discord.WebSocket


[<RequireQualifiedAccess>]
type DiscordEvent =
    | MessageReceived of message: SocketMessage
    | MessageDeleted of message: Cacheable<IMessage, uint64> * channel: ISocketMessageChannel
    | MessageUpdated of oldMessage: Cacheable<IMessage, uint64> * newMessage: SocketMessage * channel: ISocketMessageChannel
    | ReactionAdded of message: Cacheable<IUserMessage, uint64> * channel: ISocketMessageChannel * reaction: SocketReaction
    | ReactionRemoved of message: Cacheable<IUserMessage, uint64> * channel: ISocketMessageChannel * reaction: SocketReaction
    | ReactionsCleared of message: Cacheable<IUserMessage, uint64> * channel: ISocketMessageChannel
    | RoleCreated of role: SocketRole
    | RoleDeleted of role: SocketRole
    | RoleUpdated of oldRole: SocketRole * newRole: SocketRole
    | UserJoined of user: SocketGuildUser
    | UserLeft of user: SocketGuildUser
    | UserUpdated of oldUser: SocketGroupUser * newUser: SocketGuildUser

type DiscordEventContext =
    { Client: BaseSocketClient
      Event: DiscordEvent }

type DiscordFuncResult = Task<DiscordEventContext option>
type DiscordFunc = DiscordEventContext -> DiscordFuncResult
type DiscordHandler = DiscordFunc -> DiscordEventContext -> DiscordFuncResult
