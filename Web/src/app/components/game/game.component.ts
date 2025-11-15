import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { SuccessDialogComponent } from '../success-dialog/success-dialog.component';
import { GameService } from '../../services/game.service';

@Component({
  selector: 'app-game',
  standalone: true,
  imports: [CommonModule, FormsModule, MatCardModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatDialogModule, SuccessDialogComponent],
  templateUrl: './game.component.html',
  styleUrl: './game.component.scss'
})
export class GameComponent {
  sessionId: string | null = null;
  guess = 1;
  feedback: string | null = null;
  guessCount = 0;
  bestGuessCount: number | null = null;

  constructor(private game: GameService, private snack: MatSnackBar, private dialog: MatDialog) {}
  ngOnInit() {
    this.game.start().subscribe(res => this.sessionId = res.sessionId);
  }
  submit() {
    this.game.guess(this.guess).subscribe(res => {
      this.feedback = res.result;
      this.guessCount = res.guessCount;
      this.bestGuessCount = res.bestGuessCount ?? null;
      const msg = res.result === 'correct' ? 'Correct!' : res.result === 'higher' ? 'Try higher' : 'Try lower';
      this.snack.open(msg, 'Close', { duration: 1500 });
      if (res.result === 'correct') {
        this.dialog.open(SuccessDialogComponent, { data: { guessCount: res.guessCount }, width: '320px' });
      }
    });
  }
}
