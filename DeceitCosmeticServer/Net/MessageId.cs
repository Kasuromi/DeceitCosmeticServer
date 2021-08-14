namespace DeceitCosmeticServer.Net {
    public enum MessageId : byte {
        SEnterQueue = 7,
        SEnterQueueResult,
        SExitQueueResult = 10,
        SUpdateUserProfile = 21,
        SSyncParty = 29,
        SPartyUserAction = 40,
        SUpdateEmblem = 60,
        SAuthenticateOAuthClient = 62,
        SSyncOnlineProfileAttributes = 68,
        SLookupUserProfile = 90,
        SLookupUserProfileResult,
        SAuthenticateClientResult = 111
    }
}
