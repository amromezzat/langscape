import { FlashCardSetForm } from "./flashCardSetForm";
import { FlashCardWord, NewFlashCardWord } from "./flashCardWord";

export class FlashCardSetDto {
    id: string = '';
    name?: string = undefined;
    createdWords: NewFlashCardWord[] = [];
    updatedWords: FlashCardWord[] = [];
    deletedWords: string[] = [];

    constructor(setForm?: FlashCardSetForm) {
        if(setForm) {
          this.id = setForm.id!;
          this.name = setForm.name;
          setForm.words.forEach(word => {
            if(word.isDeleted) {
                this.deletedWords.push(word.id!);
            } else if (word.id != null) {
                this.updatedWords.push(word as FlashCardWord);
            } else {
                this.createdWords.push(word as NewFlashCardWord);
            }
          });   
        }
    }
}