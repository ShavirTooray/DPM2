package student.projects.leaselinkapplication

import android.app.Activity
import android.content.Intent
import android.net.Uri
import android.os.Bundle
import android.widget.*
import androidx.appcompat.app.AlertDialog
import androidx.appcompat.app.AppCompatActivity

class PaymentsActivity : AppCompatActivity() {

    private var selectedProofUri: Uri? = null
    private var selectedMethod: String? = null
    private val rentAmount = "R 12,000"

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_payments)

        val amountDueText = findViewById<TextView>(R.id.rentAmount)
        val cardButton = findViewById<Button>(R.id.button12)
        val eftButton = findViewById<Button>(R.id.button15)
        val otherButton = findViewById<Button>(R.id.button14)
        val uploadProofButton = findViewById<Button>(R.id.button16)
        val payNowButton = findViewById<Button>(R.id.button11)

        // In the new layout, the label "Amount Due" is separate, so this TextView holds only the amount.
        amountDueText.text = rentAmount

        // Keep card flow commented as in original
        // cardButton.setOnClickListener {
        //     selectedMethod = "Card"
        //     showCardDialog()
        // }

        eftButton.setOnClickListener {
            selectedMethod = "EFT"
            showEFTDialog()
        }

        otherButton.setOnClickListener {
            selectedMethod = "Other"
            showOtherDialog()
        }

        uploadProofButton.setOnClickListener {
            val intent = Intent(Intent.ACTION_GET_CONTENT)
            intent.type = "*/*"
            startActivityForResult(intent, 102)
        }

        payNowButton.setOnClickListener {
            if (selectedMethod == null) {
                Toast.makeText(this, "Please select a payment method", Toast.LENGTH_SHORT).show()
            } else {
                val fileName = selectedProofUri?.lastPathSegment ?: "No proof uploaded"
                Toast.makeText(this, "Payment of $rentAmount via $selectedMethod submitted!", Toast.LENGTH_LONG).show()
                println("Proof: $fileName")
                finish()
            }
        }
    }

    override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
        super.onActivityResult(requestCode, resultCode, data)

        if (requestCode == 102 && resultCode == Activity.RESULT_OK) {
            selectedProofUri = data?.data
            val fileName = selectedProofUri?.lastPathSegment ?: "Unknown file"
            Toast.makeText(this, "Proof uploaded: $fileName", Toast.LENGTH_SHORT).show()
        }
    }

    // private fun showCardDialog() {
    //     val dialogView = layoutInflater.inflate(R.layout.dialog_card_payment, null)
    //     AlertDialog.Builder(this)
    //         .setTitle("Enter Card Details")
    //         .setView(dialogView)
    //         .setPositiveButton("Confirm") { _, _ ->
    //             Toast.makeText(this, "Card details saved", Toast.LENGTH_SHORT).show()
    //         }
    //         .setNegativeButton("Cancel", null)
    //         .show()
    // }

    private fun showEFTDialog() {
        AlertDialog.Builder(this)
            .setTitle("EFT Instructions")
            .setMessage("Please transfer to:\nBank: LeaseLink Bank\nAccount: 123456789\nReference: Your Unit Number")
            .setPositiveButton("Done") { _, _ ->
                Toast.makeText(this, "EFT selected", Toast.LENGTH_SHORT).show()
            }
            .show()
    }

    private fun showOtherDialog() {
        val options = arrayOf("Cash", "Mobile Wallet", "Other")
        AlertDialog.Builder(this)
            .setTitle("Select Payment Method")
            .setItems(options) { _, which ->
                selectedMethod = options[which]
                Toast.makeText(this, "${options[which]} selected", Toast.LENGTH_SHORT).show()
            }
            .show()
    }
}

