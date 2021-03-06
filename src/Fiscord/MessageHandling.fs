namespace Fiscord.MessageHandling

open Discord
open FSharp.Control.Tasks
open Fiscord


type MessageHandler = IMessage -> DiscordHandler

module MessageHandlers =
    
    let compose (mHandler1: MessageHandler) (mHandler2: MessageHandler) : MessageHandler =
        fun msg ->
            (mHandler1 msg) >> (mHandler2 msg)

    let reply (text: string) : MessageHandler =
        fun msg ->
            fun next ctx -> task {
                let! _message = msg.Channel.SendMessageAsync(text)
                return! next ctx
            }
    
    let choose (msgHandlers: MessageHandler seq) : MessageHandler =
        fun msg ->
            let handlers = msgHandlers |> Seq.map (fun h -> h msg)
            DiscordHandlers.choose handlers

module Operators =
    
    let ( >=> ) h1 h2 = MessageHandlers.compose h1 h2
