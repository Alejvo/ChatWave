export interface message {
    messageId:string;
    text: string;
    originId: string;
    destinyId: string;
    sentAt: Date;
    messageType:string;
    isSentByUser:boolean;
}