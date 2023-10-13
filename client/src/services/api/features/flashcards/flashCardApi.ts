import { FlashCardSet } from "../../../../models/flashCards/flashCardSet";
import { apiRequest } from "../../core/apiRequest";

export const flashCardApi = {
    getCardSets: (filters?: Map<string, any>) => apiRequest.get<FlashCardSet[]>('/flashcards', filters),
    addFavoriteSet: (setId: string) => apiRequest.post(`/flashCards/favorites/${setId}`, {}),
    removeFavoriteSet: (setId: string) => apiRequest.del(`/flashCards/favorites/${setId}`)
}