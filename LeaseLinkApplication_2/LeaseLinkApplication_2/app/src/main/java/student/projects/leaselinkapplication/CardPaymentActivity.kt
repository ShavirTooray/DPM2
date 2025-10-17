package student.projects.leaselinkapplication

import android.os.Bundle
import android.widget.EditText
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity

class CardPaymentActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_card_payment)

        val cardNumberInput = findViewById<EditText>(R.id.cardNumberInput)
        val cardExpiryInput = findViewById<EditText>(R.id.cardExpiryInput)
        val cardCVVInput = findViewById<EditText>(R.id.cardCVVInput)

        // You can trigger this on a button click
        val cardNumber = cardNumberInput.text.toString().trim()
        val expiryDate = cardExpiryInput.text.toString().trim()
        val cvv = cardCVVInput.text.toString().trim()

        if (cardNumber.isEmpty() || expiryDate.isEmpty() || cvv.isEmpty()) {
            Toast.makeText(this, "Please fill in all card details", Toast.LENGTH_SHORT).show()
        } else {
            Toast.makeText(this, "Card details captured", Toast.LENGTH_SHORT).show()
            // You can now validate or send these details for processing
        }
    }
}
