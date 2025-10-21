package student.projects.leaselinkapplication

import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import java.util.Calendar

class CardPaymentActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_card_payment)

        val cardNumberInput = findViewById<EditText>(R.id.cardNumberInput)
        val cardExpiryInput = findViewById<EditText>(R.id.cardExpiryInput)
        val cardCVVInput = findViewById<EditText>(R.id.cardCVVInput)
        val payNowButton = findViewById<Button>(R.id.button17)

        // Read and validate when the user taps "Pay Now"
        payNowButton.setOnClickListener {
            val cardNumber = cardNumberInput.text.toString().replace("\\s".toRegex(), "")
            val expiryDate = cardExpiryInput.text.toString().trim()
            val cvv = cardCVVInput.text.toString().trim()

            // Basic presence checks
            if (cardNumber.isEmpty() || expiryDate.isEmpty() || cvv.isEmpty()) {
                Toast.makeText(this, "Please fill in all card details", Toast.LENGTH_SHORT).show()
                return@setOnClickListener
            }

            // Card number: digits only, typical length 12–19, and Luhn check
            if (!cardNumber.matches(Regex("^\\d{12,19}$")) || !luhnCheck(cardNumber)) {
                Toast.makeText(this, "Invalid card number", Toast.LENGTH_SHORT).show()
                return@setOnClickListener
            }

            // Expiry: MM/YY format and not in the past
            if (!isValidExpiry(expiryDate)) {
                Toast.makeText(this, "Invalid expiry (use MM/YY)", Toast.LENGTH_SHORT).show()
                return@setOnClickListener
            }

            // CVV: 3 or 4 digits
            if (!cvv.matches(Regex("^\\d{3,4}$"))) {
                Toast.makeText(this, "Invalid CVV", Toast.LENGTH_SHORT).show()
                return@setOnClickListener
            }

            val last4 = cardNumber.takeLast(4)
            Toast.makeText(this, "Card •••• $last4 captured", Toast.LENGTH_SHORT).show()
            // Proceed with processing or return result to previous screen if needed.
        }
    }

    private fun luhnCheck(number: String): Boolean {
        var sum = 0
        var alternate = false
        for (i in number.length - 1 downTo 0) {
            var n = number[i] - '0'
            if (alternate) {
                n *= 2
                if (n > 9) n -= 9
            }
            sum += n
            alternate = !alternate
        }
        return sum % 10 == 0
    }

    private fun isValidExpiry(exp: String): Boolean {
        // Expect MM/YY
        val match = Regex("^(0[1-9]|1[0-2])/\\d{2}$").matchEntire(exp) ?: return false
        val (mmStr, yyStr) = match.value.split("/")
        val month = mmStr.toInt()
        val yearTwo = yyStr.toInt()

        // Convert YY to 20YY (simple assumption for current century)
        val cal = Calendar.getInstance()
        val currentYearTwo = cal.get(Calendar.YEAR) % 100
        val currentMonth = cal.get(Calendar.MONTH) + 1 // Calendar.MONTH is 0-based

        // Expired if year < current, or same year but month < current
        return when {
            yearTwo > currentYearTwo -> true
            yearTwo < currentYearTwo -> false
            else -> month >= currentMonth
        }
    }
}
