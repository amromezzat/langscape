import { FlashCardSet } from "../../../../models/flashCards/flashCardSet";
import { apiRequest } from "../../core/apiRequest";

export const flashCardApi = {
    get: () => apiRequest.get<FlashCardSet[]>('/flashcards')
}