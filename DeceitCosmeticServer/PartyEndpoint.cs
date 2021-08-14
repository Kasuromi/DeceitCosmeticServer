using DeceitCosmeticServer.Net;
using DeceitCosmeticServer.Net.Messages;
using DeceitCosmeticServer.Net.Models;
using DeceitCosmeticServer.Net.Models.Enums;
using DeceitCosmeticServer.Net.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace DeceitCosmeticServer {
    public class PartyEndpoint : WebSocketBehavior {
        private SUserProfile _profile = null;
        private Guid _partyGuid = Guid.NewGuid();
        protected override void OnOpen() {
            Console.WriteLine($"Received connection from {Context.UserEndPoint.Address.MapToIPv4()}");
        }
        protected override void OnMessage(MessageEventArgs e) {
            MessageId messageId = (MessageId)e.RawData[0];
            SNetworkReader reader = new(e.RawData);
            switch (messageId) {
                case MessageId.SEnterQueue:
                    SendMessage("You cannot queue on the Deceit Cosmetic Server.\nThe server's purpose is to allow users to view cosmetics, but not play with them.\n\n - With <3 from Kasuromi");
                    break;
                case MessageId.SUpdateEmblem:
                    if (_profile == null) return;
                    SUpdateEmblem emblemUpdate = (SUpdateEmblem)new SUpdateEmblem().Deserialize(reader);
                    _profile.EmblemId = emblemUpdate.EmblemId;
                    Send(new SUpdateUserProfile {
                        RequestId = emblemUpdate.RequestId,
                        UserProfile = _profile,
                        IsCurrentUser = true
                    }.Serialize());
                    break;
                case MessageId.SAuthenticateOAuthClient:
                    SAuthenticateOAuthClient authMessage = (SAuthenticateOAuthClient)new SAuthenticateOAuthClient().Deserialize(reader);
                    Console.WriteLine($"Received authentication request for {authMessage.Username}. Game Version: {authMessage.GameVersion}");
                    _profile = new SUserProfile {
                        Username = "Cosmetic Server",
                        EmblemId = 0,
                        Unlocks = Program.UNLOCKS,
                        ServerParams = new List<SUserProfileParam> {
                            SUserProfileParam.New<int>("level", 0),
                            SUserProfileParam.New<int>("reputation", -1),
                        }
                    };
                    Send(new SPartyUserAction {
                        PartyGuid = _partyGuid,
                        Action = EPartyUserAction.ePUA_PlayerPromoted
                    }.Serialize());
                    Send(new SSyncParty {
                        PartyGuid = _partyGuid,
                        UserProfile = _profile
                    }.Serialize());
                    Send(new SAuthenticateClientResult {
                        RequestId = authMessage.RequestId,
                        UserProfile = _profile
                    }.Serialize());
                    Send(new SUpdateUserProfile {
                        RequestId = authMessage.RequestId,
                        UserProfile = _profile,
                        IsCurrentUser = true
                    }.Serialize());
                    Send(new SExitQueueResult { Succeeded = true }.Serialize());
                    SendMessage("Welcome to the Deceit Cosmetic Server.\nCosmetic choices aren't saved and will not carry over to the live servers.\n\n - With <3 from Kasuromi");
                    break;
                case MessageId.SSyncOnlineProfileAttributes:
                    if (_profile == null) return;
                    SSyncOnlineProfileAttributes syncedAttributes = (SSyncOnlineProfileAttributes)new SSyncOnlineProfileAttributes().Deserialize(reader);
                    foreach (var attribute in syncedAttributes.Attributes) {
                        var param = _profile.UserParams.FirstOrDefault((x) => x.Name == attribute.Name);
                        if (param != null) {
                            param.Value = attribute.Value;
                        } else {
                            _profile.UserParams.Add(attribute);
                        }
                    }
                    Send(new SUpdateUserProfile {
                        RequestId = syncedAttributes.RequestId,
                        UserProfile = _profile,
                        IsCurrentUser = true
                    }.Serialize());
                    break;
                case MessageId.SLookupUserProfile:
                    Send(new SLookupUserProfileResult {
                        RequestId = 0,
                        UserProfile = _profile
                    }.Serialize());
                    break;
            }
        }
        private void SendMessage(string message) {
            Send(new SEnterQueueResult {
                Succeeded = false,
                IsRanked = false,
                Message = message,
            }.Serialize());
        }
    }
}
